using Between_Stars.Classes;

using System;
using System.Collections.Generic;
using System.Xml.Linq;

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
            for (int i = 0; i < currentHangar.Inventory.Count; i++)
            {
                var c = session.Commodities.FirstOrDefault(c => c.Id == currentHangar.Inventory[i].CommodityId);
                //var c = currentHangar.Inventory[i];
                Console.WriteLine($"{i + 1}. {c.Name} - " +
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
            Console.Write("Välj vara att köpa (nummer): ");
            if (!int.TryParse(Console.ReadLine(), out int val) || val < 1 || val > session.Commodities.Count)
            {
                Console.WriteLine("Felaktigt val.");
                return;
            }

            var selected = session.Commodities[val - 1];
            Commodity foundCommodity =  session.Commodities.FirstOrDefault(c => c.Name == selected.Name);

            Console.Write($"Hur många {selected.Name} vill du köpa? ");
            if (!int.TryParse(Console.ReadLine(), out int antal) || antal < 1)
            {
                Console.WriteLine("Felaktigt antal.");
                return;
            }

            double totalPris = antal * foundCommodity.BasePrice;
            double totalVolym = antal * foundCommodity.Volume;

            if (session.LoggedInPlayer.Credits >= totalPris && session.LoggedInPlayer.CurrentCargo + totalVolym <= session.LoggedInPlayer.CargoCapacity)
            {
                session.LoggedInPlayer.Credits -= totalPris;
                session.LoggedInPlayer.CurrentCargo += totalVolym;


                var item = session.LoggedInPlayer.Cargo.FirstOrDefault(c => c.Name == foundCommodity.Name);
                if (item != null)
                    item.Amount += antal;
                else
                    session.LoggedInPlayer.Cargo.Add(new CargoItem { 
                        Name = foundCommodity.Name, 
                        Amount = antal,
                        Acronym = foundCommodity.Acronym,
                        Volume = foundCommodity.Volume});

                Console.WriteLine($"Du köpte {antal} {selected.Name}!");
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
                Console.WriteLine("Du har inte tillräckligt med pengar eller plats i lasten.");
            }
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

                Console.Write($"Hur många {selected.Name} vill du sälja? ");
                if (!int.TryParse(Console.ReadLine(), out int antal) || antal < 1)
                {
                    Console.WriteLine("Felaktigt antal.");
                    return;
                }

                double totalPris = antal * foundCommodity.BasePrice;
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
            

                Console.WriteLine($"Du Sålde {antal} {selected.Name}!");
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
            } else
            {
                Console.WriteLine("Du har inga varor att sälja");
            }
        }


    }
}
