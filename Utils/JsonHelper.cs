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
            public static List<Player> LoadPlayers()
            {
            string playerFilePath = Path.Combine("Data", "players.json");
            if (!File.Exists(playerFilePath))
                return new List<Player>(); // Eller throw, om du vill ha fel

            string json = File.ReadAllText(playerFilePath);
            return JsonSerializer.Deserialize<List<Player>>(json); // Deserialisera till lista!
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
            public static List<Mission> LoadMissions(string MissionFilePath)
            {
                string json = File.ReadAllText(MissionFilePath);
                return JsonSerializer.Deserialize<List<Mission>>(json);
            }

        public static void SavePlayers(List<Player> players)
        {
            string playerFilePath = Path.Combine("Data", "players.json");
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(players, options); // Serialisera hela listan!
            File.WriteAllText(playerFilePath, json);
        }

        public static void SaveCBs(List<CelestialBody> celestialBodies)
        {
            string celestialBodyFilePath = Path.Combine("Data", "celestialBodies.json");
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(celestialBodies, options); // Serialisera hela listan!
            File.WriteAllText(celestialBodyFilePath, json);
        }

        public static void SaveMissions(List<Mission> missions)
        {
            string missionFilePath = Path.Combine("Data", "missions.json");
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(missions, options); // Serialisera hela listan!
            File.WriteAllText(missionFilePath, json);
        }
    }
}

