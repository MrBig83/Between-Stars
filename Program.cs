using Between_Stars.Classes;
using Between_Stars.Utils;
using System;
using System.Numerics;

namespace Between_Stars
{
    internal partial class Program //: AccountHandler
    {
        static void Main(string[] args)
        {
            // OBS: "Data/ships.json" söker från *bin-mappen* (där .exe körs)
            string shipFilePath = Path.Combine("Data", "ships.json");
            string commodityFilePath = Path.Combine("Data", "commodities.json");
            string celestialBodyFilePath = Path.Combine("Data", "celestialBodies.json");
            string playerFilePath = Path.Combine("Data", "players.json");

            List<Ship> ships = JsonHelper.LoadShips(shipFilePath);
            List<Commodity> commodities = JsonHelper.LoadCommodities(commodityFilePath);
            List<CelestialBody> celestialBodies = JsonHelper.LoadCelestialBodies(celestialBodyFilePath);
            List<Player> players = JsonHelper.LoadPlayer(playerFilePath);

            //LogInUser();

            string userName = "Mr.Big";
            var loggedInPlayer = players.SingleOrDefault(p => p.Name == userName);
                //Sätt loggedInPlayer till den som har samma username som när man startade spelet. 

            
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



            // LINQ – FirstOrDefault (hämtar första matchen eller null)
            Ship playerShip = ships.FirstOrDefault(s => s.ShipId == loggedInPlayer.ShipId);
            if (playerShip != null)
            {
                Console.WriteLine($"Du sitter i skepp: {playerShip.Name}");
            }
            else
            {
                Console.WriteLine("Inget skepp hittades med det id:t.");
            }


            Console.WriteLine("Tillgängliga varor i universum: ");
            commodities.ForEach(c => Console.WriteLine(c.Name));

          
            Console.WriteLine("Nya i lasten:");
            loggedInPlayer.Cargo.ToList().ForEach(c => Console.WriteLine($"{c.Value}st {c.Key}"));


            GameManager gameManager = new GameManager(loggedInPlayer, commodities);  // Skapa en instans
            gameManager.StartGame();

            //Visa huvudmeny
            //MarketHandler.BuyCommodity(loggedInPlayer, commodities);
            
        }
    }
}

