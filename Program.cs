using Between_Stars.Classes;
using Between_Stars.Utils;
using System;
using System.Numerics;

namespace Between_Stars
{
    internal partial class Program
    {
        static void Main(string[] args)
        {
            // OBS: "Data/ships.json" söker från *bin-mappen* (där .exe körs)
            string shipFilePath = Path.Combine("Data", "ships.json");
            string commodityFilePath = Path.Combine("Data", "commodities.json");
            string celestialBodyFilePath = Path.Combine("Data", "celestialBodies.json");
            string playerFilePath = Path.Combine("Data", "players.json");

            List<PlayerShip> ships = JsonHelper.LoadShips(shipFilePath);
            List<Commodity> commodities = JsonHelper.LoadCommodities(commodityFilePath);
            List<CelestialBody> celestialBodies = JsonHelper.LoadCelestialBodies(celestialBodyFilePath);
            List<Player> players = JsonHelper.LoadPlayer(playerFilePath);

            var loggedInPlayer = players[0];
            //var currentStation = celestialBodies[0];
            var currentStation = celestialBodies.First(s => s.Id == loggedInPlayer.CurrentLocationId);
            Console.WriteLine($"Nuvarande position: {currentStation.Name}");
            //ShowStationMarket(currentStation);

            var commodityDict = commodities.ToDictionary(c => c.Id);
            Console.WriteLine("Stationens inventarie:\n");
            foreach (var entry in currentStation.Inventory)
            {
                if (commodityDict.TryGetValue(entry.CommodityId, out var commodity))
                {
                    Console.WriteLine($"{commodity.Name} – {entry.Stock} st – Pris: {entry.Price} credits");
                }
            }
            Console.WriteLine("\n------- Slut på inventarie -------\n");

            PlayerShip Ship = ships[0]; // Ta första skeppet
            
            Console.WriteLine($"Skepp: {Ship.Name}, Bränsle: {Ship.CurrentFuel}/{Ship.FuelCapacity}, Tier: {Ship.Tier}");
            Console.WriteLine("Tillgängliga varor i universum: ");
            foreach (var item in commodities)
            {
                Console.WriteLine($"{item.Name}, {item.Acronym}, {item.BasePrice}");
            }
            var player = players[0];
            foreach (var item in player.CurrentCargo)
            {
                Console.WriteLine($"I lasten: {item.Key}: {item.Value} st");
            }

            // ================================================================
            //Player player = new Player();
            //player.Name = "D4ddie";
            //player.Ship = Ship; // "Ship" är din PlayerShip från tidigare

            //Console.WriteLine($"Kapten: {player.Name}");
            //Console.WriteLine($"Krediter: {player.Credits}");
            //Console.WriteLine($"Rykte: {player.Reputation}");
            //Console.WriteLine($"Skepp: {player.Ship.Name}, Last: {player.Ship.CurrentCargo}/{player.Ship.CargoCapacity}");


            // Ladda commodities från JSON eller skapa en lista manuellt
            //List<Commodity> commodities = new List<Commodity>
            //{
            //    new Commodity { Name = "Titanium", Acronym = "Tit", Price = 120, Volume = 1 },
            //    new Commodity { Name = "Water", Acronym = "Wat", Price = 20, Volume = 0.5 }
            //    // osv...
            //};

            GameManager gameManager = new GameManager();  // Skapa en instans
            //gameManager.StartGame();

            //Visa huvudmeny
            MarketHandler.BuyCommodity(player, commodities);
            //MarketHandler.SellCommodity(player, commodities);
        }

        //string text = File.ReadAllText(@"./Data/ships.json");
        //    var ship = JsonSerializer.Deserialize<Ship>(text);

        //    Console.WriteLine($"Ship Name: {ship.Name}");
        //    Console.WriteLine($"Fuel Capacity: {ship.FuelCapacity}");
        //    Console.WriteLine($"Current fuel: {ship.CurrentFuel}");
        //    Console.WriteLine($"Ship Tier: {ship.Tier}");
    }
}

