using Between_Stars.Classes;
using System.Text.Json;

namespace Between_Stars.Utils
{

        public static class JsonHelper
        {
            //public static List<Player> SavePlayers(string playerFilePath)
            //{
            //string json = File.ReadAllText(playerFilePath);
            //    return JsonSerializer.Deserialize<List<Player>>(json);
            //}
            public static List<Player> LoadPlayer(string playerFilePath)
            {
            string json = File.ReadAllText(playerFilePath);
            return JsonSerializer.Deserialize<List<Player>>(json);
            }

        public static List<Ship> LoadShips(string filePath)
            {
                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<Ship>>(json);
            }

            public static List<Commodity> LoadCommodities(string commodityFilePath)
            {
                string json = File.ReadAllText(commodityFilePath);
                return JsonSerializer.Deserialize<List<Commodity>>(json);
            }

            public static List<CelestialBody> LoadCelestialBodies(string celestialBodyFilePath)
            {
                string json = File.ReadAllText(celestialBodyFilePath);
                return JsonSerializer.Deserialize<List<CelestialBody>>(json);
            }

            public static void SavePlayer(string filePath, Player player)
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true // gör JSON snyggt och lättläst
                };
                // Spara som lista om du har flera spelare, annars direkt som objekt
                string json = JsonSerializer.Serialize(player, options);
                File.WriteAllText(filePath, json);
            }
        }
}

