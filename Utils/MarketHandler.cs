using Between_Stars.Classes;

using System;
using System.Collections.Generic;

namespace Between_Stars.Utils
{
    public static class MarketHandler
    {
        // Visa vilka varor som finns tillgängliga för köp
        public static void ShowMarket(List<Commodity> commodities)
        {
            Console.WriteLine("---- Marknaden ----");
            for (int i = 0; i < commodities.Count; i++)
            {
                var c = commodities[i];
                Console.WriteLine($"{i + 1}. {c.Name} - Pris: {c.Price} cr - Volym: {c.Volume} m³");
            }
        }

        // Hantera köp av vara
        public static void BuyCommodity(Player player, List<Commodity> commodities)
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

            double totalPris = antal * selected.Price;
            double totalVolym = antal * selected.Volume;

            if (player.Credits >= totalPris && player.Ship.CurrentCargo + totalVolym <= player.Ship.CargoCapacity)
            {
                player.Credits -= totalPris;
                player.Ship.CurrentCargo += totalVolym;
                Console.WriteLine($"Du köpte {antal} {selected.Name}!");
                Console.WriteLine($"Kvar: {player.Credits} cr, Last: {player.Ship.CurrentCargo}/{player.Ship.CargoCapacity} m³");
                // Här kan du lägga till i inventory om du vill spara vad spelaren har.
            }
            else
            {
                Console.WriteLine("Du har inte tillräckligt med pengar eller plats i lasten.");
            }
        }
    }
}
