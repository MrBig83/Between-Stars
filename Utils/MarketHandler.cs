using Between_Stars.Classes;

using System;
using System.Collections.Generic;

namespace Between_Stars.Utils
{
    public class MarketHandler
    {
        private List<Commodity> commodities;
        public MarketHandler(List<Commodity> commodities)
        {
            this.commodities = commodities;
        }

        // Visa vilka varor som finns tillgängliga för köp ======= BYT UT TILL DICTIONARY ========
        public static void ShowMarket(List<Commodity> commodities)
        {
            Console.WriteLine("---- Marknaden ----");
            for (int i = 0; i < commodities.Count; i++)
            {
                var c = commodities[i];
                Console.WriteLine($"{i + 1}. {c.Name} - Pris: {c.BasePrice} cr - Volym: {c.Volume} m³");
            }
        }

        public static void ShowCargo(Player player)
        {
            Console.WriteLine("Din nuvarande last:");
            if(player.CurrentCargo == 0)
            {
                Console.WriteLine("Du har inget i lasten");
            }
            else
            {
                int index = 1;
                foreach (var item in player.Cargo)
                {
                    Console.WriteLine($"{index++}. {item.Key} - {item.Value} st");
                }
            }
        }

        // Hantera köp av vara
        public void BuyCommodity(Player player)
        {
            ShowMarket(commodities);
            Console.Write("Välj vara att köpa (nummer): ");
            if (!int.TryParse(Console.ReadLine(), out int val) || val < 1 || val > commodities.Count)
            {
                Console.WriteLine("Felaktigt val.");
                return;
            }

            var selected = commodities[val - 1];
            Console.Write($"Hur många {selected.Name} vill du köpa? ");
            if (!int.TryParse(Console.ReadLine(), out int antal) || antal < 1)
            {
                Console.WriteLine("Felaktigt antal.");
                return;
            }

            double totalPris = antal * selected.BasePrice;
            double totalVolym = antal * selected.Volume;

            if (player.Credits >= totalPris && player.CurrentCargo + totalVolym <= player.CargoCapacity)
            {
                player.Credits -= totalPris;
                player.CurrentCargo += totalVolym;

                //Dictionary<string, int> Cargo = new Dictionary<string, int>();
                //Cargo.Add(selected.Name, antal );
                if(player.Cargo.ContainsKey(selected.Name))
                {
                    player.Cargo[selected.Name] += antal;
                } else
                {
                    player.Cargo[selected.Name] = antal;
                }

                    Console.WriteLine($"Du köpte {antal} {selected.Name}!");
                Console.WriteLine($"Kvar: {player.Credits} cr, Last: {player.CurrentCargo}/{player.CargoCapacity} m³");
                // Här kan du lägga till i inventory om du vill spara vad spelaren har.
                JsonHelper.SavePlayer("players.json", player);
            }
            else
            {
                Console.WriteLine("Du har inte tillräckligt med pengar eller plats i lasten.");
            }
        }

        public void SellCommodity(Player player)
        {
            //ShowMarket(commodities);
            ShowCargo(player);
            Console.Write("Välj vara att sälja (nummer): ");
            if (!int.TryParse(Console.ReadLine(), out int val) || val < 1 || val > commodities.Count)
            {
                Console.WriteLine("Felaktigt val.");
                return;
            }

            var selected = commodities[val - 1];
            Console.Write($"Hur många {selected.Name} vill du sälja? ");
            if (!int.TryParse(Console.ReadLine(), out int antal) || antal < 1)
            {
                Console.WriteLine("Felaktigt antal.");
                return;
            }

            double totalPris = antal * selected.BasePrice;
            double totalVolym = antal * selected.Volume;

            //if (player.Credits >= totalPris && player.Ship.CurrentCargo + totalVolym <= player.Ship.CargoCapacity)
            //{
                player.Credits += totalPris;
                player.CurrentCargo -= totalVolym;

            if (player.Cargo.ContainsKey(selected.Name) && player.Cargo[selected.Name] >= antal)
            {
                player.Cargo[selected.Name] -= antal;
                if (player.Cargo[selected.Name] == 0)
                {
                    player.Cargo.Remove(selected.Name);
                }
            }
            else
            {
                Console.WriteLine("Du har inte tillräckligt många av denna vara för att sälja.");
            }


            Console.WriteLine($"Du Sålde {antal} {selected.Name}!");
                Console.WriteLine($"Kvar: {player.Credits} cr, Last: {player.CurrentCargo}/{player.CargoCapacity} m³");
                // Här kan du lägga till i inventory om du vill spara vad spelaren har.
            //}
            //else
            //{
            //    Console.WriteLine("Du har inte tillräckligt med pengar eller plats i lasten.");
            //}
        }
    }
}
