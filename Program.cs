using Between_Stars.Classes;
using Between_Stars.Utils;
using System;
using System.Numerics;
using static System.Collections.Specialized.BitVector32;

namespace Between_Stars
{
    internal partial class Program //: AccountHandler
    {
        static void Main(string[] args)
        {
            string shipFilePath = Path.Combine("Data", "ships.json");
            string commodityFilePath = Path.Combine("Data", "commodities.json");
            string celestialBodyFilePath = Path.Combine("Data", "celestialBodies.json");
            string playerFilePath = Path.Combine("Data", "players.json");

            List<Ship> ships = JsonHelper.LoadShips(shipFilePath);
            List<Commodity> commodities = JsonHelper.LoadCommodities(commodityFilePath);
            List<CelestialBody> celestialBodies = JsonHelper.LoadCelestialBodies(celestialBodyFilePath);
            List<Player> players = JsonHelper.LoadPlayers();

            //LogInUser();

            string userName = "Mr.Big";
            var loggedInPlayer = players.SingleOrDefault(p => p.Name == userName);
                //Sätt loggedInPlayer till den som har samma username som när man startade spelet. 

            
            var currentStation = celestialBodies.First(s => s.Id == loggedInPlayer.CurrentLocationId);
            Console.WriteLine($"Nuvarande position: {currentStation.Name}");

            //var commodityDict = commodities.ToDictionary(c => c.Id);
            //Console.WriteLine("Stationens inventarie:\n");
            //foreach (var entry in currentStation.Inventory)
            //{
            //    if (commodityDict.TryGetValue(entry.CommodityId, out var commodity))
            //    {
            //        Console.WriteLine($"{commodity.Name} – {entry.Stock} st – Pris: {entry.Price} credits");
            //    }
            //}

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
            loggedInPlayer.Cargo.ToList().ForEach(c => Console.WriteLine($"{c.Amount}st {c.Name}"));

            var sessionData = new SessionData
            {
                LoggedInPlayer = loggedInPlayer,
                CelestialBodies = celestialBodies,
                Commodities = commodities,
                Ships = ships,
                // ...lägg till det du behöver
            };
            //sessionData.MarketHandler = new MarketHandler(sessionData);
            bool gameRunning = true;

            
            GameManager gameManager = new GameManager(sessionData);  // Skapa en instans
            while(gameRunning)
            {
                gameRunning = gameManager.StartGame();
            }


            
        }
    }
}

