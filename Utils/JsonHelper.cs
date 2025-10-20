using Between_Stars.Classes;
using System.Text.Json;

namespace Between_Stars.Utils
{

        public static class JsonHelper
        {


            public static List<PlayerShip> LoadShips(string filePath)
            {
                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<PlayerShip>>(json);
            }
        }


        
    }

