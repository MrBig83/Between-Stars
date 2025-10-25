using Between_Stars.Classes;

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Channels;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;

namespace Between_Stars.Utils
{
    public class MarketHandler
    {


        //    private List<Commodity> commodities;
        //    public MarketHandler(List<Commodity> commodities)
        //    {
        //        this.commodities = commodities;
        //    }

        //    // Visa vilka varor som finns tillgängliga för köp ======= BYT UT TILL DICTIONARY ========
        public static void ShowMarket(SessionData session)
        {

            var currentHangar = session.CelestialBodies.FirstOrDefault(c => c.Id == session.LoggedInPlayer.CurrentLocationId);
            Console.WriteLine($"Marknaden på {currentHangar.Name}:\n");
            for (int i = 0; i < currentHangar.Inventory.Count; i++)
            {
                var c = session.Commodities.FirstOrDefault(c => c.Id == currentHangar.Inventory[i].CommodityId);
                //var c = currentHangar.Inventory[i];
                Console.WriteLine($"{i+1}. {c.Name} - " +
                    $"Pris: {c.BasePrice * currentHangar.Inventory[i].PriceFactor} cr - " +
                    $"Volym/enh: {c.Volume} m³ - " +
                    $"I lager: {currentHangar.Inventory[i].Stock}");
            }

        }

        public static void ShowCargo(SessionData session)
        {
            Console.WriteLine("Din nuvarande last:");
            if (session.LoggedInPlayer.CurrentCargo == 0)
            {
                Console.WriteLine("Du har inget i lasten");
            }
            else
            {
                int index = 1;
                foreach (var item in session.LoggedInPlayer.Cargo)
                {
                    Console.WriteLine($"{index++}. {item.Name} - {item.Amount} st");
                }
            }
        }

        //    // Hantera köp av vara
        public static void BuyCommodity(SessionData session)
        {
            ShowMarket(session);
            Console.Write("Välj vara att köpa (nummer) eller skriv 'x' för att avbryta: ");
            var currentStation = session.CelestialBodies.FirstOrDefault(s => s.Id == session.LoggedInPlayer.CurrentLocationId);

            string userInput = Console.ReadLine().ToLower();

            if (userInput == "x")
            {
                Console.Clear();
                //runShowAll = false;
            }
            else
            {
                int index;
                if (int.TryParse(userInput, out index))
                {
                    index -= 1;

                    if (index >= 0 && index < currentStation.Inventory.Count) // =========== Håll koll på Index-siffran här. Testning behövs
                    {

                        var selected = currentStation.Inventory[index];
                        Commodity foundCommodity = session.Commodities.FirstOrDefault(c => c.Id == selected.CommodityId);

                        Console.Write($"Hur många {foundCommodity.Name} vill du köpa? "); //('m' för max)
                        if (!int.TryParse(Console.ReadLine(), out int antal) || antal < 1)
                        {
                            Console.WriteLine("Felaktigt antal.");
                            return;
                        }

                        double totalPris = antal * (foundCommodity.BasePrice * selected.PriceFactor);
                        double totalVolym = antal * foundCommodity.Volume;

                        double cargoVolume = 0;
                        foreach (var item in session.LoggedInPlayer.Cargo)
                        {
                            cargoVolume = cargoVolume + item.Amount * item.Volume;
                        }

                        if (session.LoggedInPlayer.Credits >= totalPris && cargoVolume + totalVolym <= session.LoggedInPlayer.CargoCapacity && selected.Stock >= antal)
                        {
                            session.LoggedInPlayer.Credits -= totalPris;
                            session.LoggedInPlayer.CurrentCargo += totalVolym;
                            selected.Stock -= antal;


                            var item = session.LoggedInPlayer.Cargo.FirstOrDefault(c => c.Name == foundCommodity.Name);
                            if (item != null)
                                item.Amount += antal;
                            else
                            {
                                session.LoggedInPlayer.Cargo.Add(new CargoItem
                                {
                                    Name = foundCommodity.Name,
                                    Amount = antal,
                                    Acronym = foundCommodity.Acronym,
                                    Volume = foundCommodity.Volume
                                });
                            }


                            Console.WriteLine($"Du köpte {antal} {foundCommodity.Name}!");
                            Console.WriteLine($"Kvar: {session.LoggedInPlayer.Credits} cr, Last: {session.LoggedInPlayer.CurrentCargo}/{session.LoggedInPlayer.CargoCapacity} m³");




                            // Hitta och uppdatera rätt spelare
                            var players = JsonHelper.LoadPlayers();
                            var playerToUpdate = players.FirstOrDefault(p => p.Id == session.LoggedInPlayer.Id);
                            if (playerToUpdate != null)
                            {
                                // Uppdatera alla properties från session.LoggedInPlayer till playerToUpdate
                                playerToUpdate.Cargo = session.LoggedInPlayer.Cargo;
                                playerToUpdate.Credits = session.LoggedInPlayer.Credits;
                                // ...alla andra properties du vill spara!
                            }
                            else
                            {
                                // Om inte finns: lägg till!
                                players.Add(session.LoggedInPlayer);
                            }

                            // Spara hela listan
                            JsonHelper.SavePlayers(players);






                            //JsonHelper.SavePlayers(session.LoggedInPlayer);
                        }
                        else
                        {
                            Console.WriteLine("Du har inte tillräckligt med pengar, plats i lasten eller så finns inte antalet du eftersöker.");
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("\nNumret du angav finns inte. Försök igen.\n");
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\nFelaktig inmatning. Ange ett giltigt nummer eller 'x' för att gå tillbaka\n");
                }
            }







            //if (!int.TryParse(Console.ReadLine(), out int val) || val < 1 || val > currentStation.Inventory.Count);
            //{
            //    Console.WriteLine("Felaktigt val.");
            //    return;
            //}

            //var selected = session.Commodities[index];
            //Commodity foundCommodity =  session.Commodities.FirstOrDefault(c => c.Name == selected.Name);

            
        }

        public static void SellCommodity(SessionData session)
        {
            if (session.LoggedInPlayer.Cargo.Count != 0)
            {
                ShowCargo(session);
                Console.Write("Välj vara att sälja (nummer): ");
                if (!int.TryParse(Console.ReadLine(), out int val) || val < 1 || val > session.LoggedInPlayer.Cargo.Count)
                {
                    Console.WriteLine("Felaktigt val.");
                    return;
                }


                var selected = session.LoggedInPlayer.Cargo[val - 1];
                Commodity foundCommodity = session.Commodities.FirstOrDefault(c => c.Name == selected.Name);
                var currentStation = session.CelestialBodies.FirstOrDefault(cb => cb.Id == session.LoggedInPlayer.CurrentLocationId);
                var localCommodity = currentStation.Inventory.FirstOrDefault(c => c.CommodityId == foundCommodity.Id);

                //Commodity localCommodity = 

                Console.Write($"Hur många {selected.Name} vill du sälja? ");
                if (!int.TryParse(Console.ReadLine(), out int antal) || antal < 1)
                {
                    Console.WriteLine("Felaktigt antal.");
                    return;
                }
                //var localCommodity = 
                //session.LoggedInPlayer.CurrentLocationId

                //currentStation.Inventory[index];


                double totalPris = antal * (foundCommodity.BasePrice * localCommodity.PriceFactor);
                double totalVolym = antal * foundCommodity.Volume;

                session.LoggedInPlayer.Credits += totalPris;
                session.LoggedInPlayer.CurrentCargo -= totalVolym;

                // Hitta rätt cargo-item
                var item = session.LoggedInPlayer.Cargo.FirstOrDefault(c => c.Name == selected.Name);

                // Kontrollera att spelaren har tillräckligt många
                if (item != null && item.Amount >= antal)
                {
                    item.Amount -= antal;
                    if (item.Amount <= 0)
                        session.LoggedInPlayer.Cargo.Remove(item);
                }
                else
                {
                    Console.WriteLine("Du har inte tillräckligt många av den varan i lasten.");
                }


                Console.WriteLine($"Du Sålde {antal} {selected.Name} för {totalPris} cr!");
                Console.WriteLine($"Kvar: {session.LoggedInPlayer.Credits} cr, Last: {session.LoggedInPlayer.CurrentCargo}/{session.LoggedInPlayer.CargoCapacity} m³");

                SaveGame(session);



                //JsonHelper.SavePlayers(session.LoggedInPlayer);
            }
            else
            {
                Console.WriteLine("Du har inga varor att sälja");
            }
        }

        public static void SaveGame(SessionData session)
        {
            // Hitta och uppdatera rätt spelare
            var players = JsonHelper.LoadPlayers();
            var playerToUpdate = players.FirstOrDefault(p => p.Id == session.LoggedInPlayer.Id);
            if (playerToUpdate != null)
            {
                // Uppdatera alla properties från session.LoggedInPlayer till playerToUpdate
                playerToUpdate.Cargo = session.LoggedInPlayer.Cargo;
                playerToUpdate.Credits = session.LoggedInPlayer.Credits;
                playerToUpdate.CurrentLocationId = session.LoggedInPlayer.CurrentLocationId;
                // ...alla andra properties du vill spara!
            }
            else
            {
                // Om inte finns: lägg till!
                players.Add(session.LoggedInPlayer);
            }

            // Spara hela listan
            JsonHelper.SavePlayers(players);
        }

        public static void Restock(SessionData session)
        {
            foreach (var station in session.CelestialBodies)
            {
                station.Inventory
                .Where(i => i.Stock < i.MaxStock)
                .ToList()
                .ForEach(i => i.Stock += 1); // eller byt ut 1 mot valfritt antal per tick ( ======= RestockRate som property på CelestialBody i framtiden)
   
            }
            JsonHelper.SaveCBs(session.CelestialBodies);
        }

        public static int GetAnalysisPrice(SessionData session)
        {
            int basePrice = 500;

            // Rykte (range 0-100), billigare med högt rykte
            int reputationDiscount = (int)(session.LoggedInPlayer.Reputation * 2.5); // 0-250 cr rabatt

            // Avstånd till New Babbage
            var nb = session.CelestialBodies.FirstOrDefault(s => s.Name == "New Babbage");
            var current = session.CelestialBodies.First(cb => cb.Id == session.LoggedInPlayer.CurrentLocationId);
            double Dist = Math.Sqrt(
                Math.Pow(nb.Position.X - current.Position.X, 2) +
                Math.Pow(nb.Position.Y - current.Position.Y, 2) +
                Math.Pow(nb.Position.Z - current.Position.Z, 2)
            );
            int distancePenalty = (int)(Dist * 5); // Justera “5” för hur mycket du vill att distans påverkar

            int price = basePrice - reputationDiscount + distancePenalty;
            return Math.Max(200, price); // Minsta pris 200 cr
        }

        public static void MarketAnalysis(SessionData session)
        {
            Console.WriteLine("MARKNADSANALYS:");
            foreach (var station in session.CelestialBodies)
            {
                Console.WriteLine($"\nStation: {station.Name}");
                foreach (var item in station.Inventory)
                {
                    var commodity = session.Commodities.FirstOrDefault(c => c.Id == item.CommodityId);
                    double pris = commodity.BasePrice * item.PriceFactor;
                    Console.WriteLine($"{commodity.Name}: {item.Stock}/{item.MaxStock} i lager – Pris: {pris:F0} cr");
                }
            }

        }


    }
}
