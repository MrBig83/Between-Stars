using Between_Stars.Classes;
using Between_Stars.Utils;
using System;

namespace Between_Stars
{
    internal partial class Program
    {
        static void Main(string[] args)
        {
            // OBS: "Data/ships.json" söker från *bin-mappen* (där .exe körs)
            string filePath = Path.Combine("Data", "ships.json");

            List<PlayerShip> ships = JsonHelper.LoadShips(filePath);

            PlayerShip Ship = ships[0]; // Ta första skeppet
            Console.WriteLine($"Skepp: {Ship.Name}, Bränsle: {Ship.CurrentFuel}/{Ship.FuelCapacity}, Tier: {Ship.Tier}");


            Player player = new Player();
            player.Name = "D4ddie";
            player.Ship = Ship; // "Ship" är din PlayerShip från tidigare

            Console.WriteLine($"Kapten: {player.Name}");
            Console.WriteLine($"Krediter: {player.Credits}");
            Console.WriteLine($"Rykte: {player.Reputation}");
            Console.WriteLine($"Skepp: {player.Ship.Name}, Last: {player.Ship.CurrentCargo}/{player.Ship.CargoCapacity}");


            // Ladda commodities från JSON eller skapa en lista manuellt
            List<Commodity> commodities = new List<Commodity>
            {
                new Commodity { Name = "Titanium", Price = 120, Volume = 1 },
                new Commodity { Name = "Water", Price = 20, Volume = 0.5 }
                // osv...
            };

            MarketHandler.BuyCommodity(player, commodities);
        }

        //string text = File.ReadAllText(@"./Data/ships.json");
        //    var ship = JsonSerializer.Deserialize<Ship>(text);

        //    Console.WriteLine($"Ship Name: {ship.Name}");
        //    Console.WriteLine($"Fuel Capacity: {ship.FuelCapacity}");
        //    Console.WriteLine($"Current fuel: {ship.CurrentFuel}");
        //    Console.WriteLine($"Ship Tier: {ship.Tier}");
    }
    }

