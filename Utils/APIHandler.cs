using Between_Stars.Classes;
using OpenAI;
using OpenAI.Chat;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Between_Stars.Utils
{
    public class APIHandler
    {
        private static readonly HttpClient client = new HttpClient();

        // Gör meny-metoden async:
        public static async Task VisaMeny(SessionData session)
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("1. Prata med AI");
                Console.WriteLine("2. Avsluta");
                string val = Console.ReadLine();
                switch (val)
                {
                    case "1":
                        await CallOpenAI(session); 
                        break;
                    case "2":
                        running = false;
                        break;
                }
            }
        }
        public static async Task CallOpenAI(SessionData session)
        {

     
            string apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
            if (string.IsNullOrEmpty(apiKey))
            {
                Console.WriteLine("API key not found");
                return;
            }

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

            Console.WriteLine("Fråga ChatGPT något:");
            //string userInput = Console.ReadLine();

            string userInput = $@"
            Skapa ett uppdrag i JSON-format med följande egenskaper:
            - ""Title"": (uppdragstitel)
            - ""Description"": (tydlig och inspirerande beskrivning på svenska)
            - ""Commodity"": (exakt namn slumpmässigt valt från listan: Titanium, Water, Food, Medical Supplies, Quantum Fuel, Ore, Textiles, Machinery, Electronics, Luxury Goods)            
            - ""Amount"": (slumpmässigt heltal antal enheter av varan (1-{session.LoggedInPlayer.CargoCapacity}))
            - ""From_station"": (exakt namn slumpmässigt från listan: New Babbage, MIC-L1, MIC-L2, Port Olisar, Grim Hex, ArcCorp, Everus Harbor, Lorville, Orison, Refinery Station R&R-001)
            - ""To_station"": (exakt namn slumpmässigt från listan, annan än from_station)
            - ""Reward_cr"": (slumpmässigt heltal mellan 500-2500)
            - ""Reward_reputation"": (slumpmässigt heltal mellan 1–5)
            Svara ENDAST med giltig, minimerad JSON och ingen annan text.";

            var requestBody = new
            {
                model = "gpt-4.1-mini",
                messages = new[]
                {
                new { role = "system", content = "You are a helpful assistant," },
                new { role = "user", content = userInput }
                }
            };

            string json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content); // =========== Lägg till en loading-spinnr här
            string responseString = await response.Content.ReadAsStringAsync();

            using JsonDocument doc = JsonDocument.Parse(responseString);
            string reply = doc.RootElement
                                .GetProperty("choices")[0]
                                .GetProperty("message")
                                .GetProperty("content")
                                .GetString();

            string cleanedJson = reply
            .Replace("```json", "")
            .Replace("```", "")
            .Trim();

            Mission cleanReply = JsonSerializer.Deserialize<Mission>(cleanedJson);
            Console.WriteLine("Tillgängligt uppdrag:");
            Console.WriteLine(cleanReply.Title);
            Console.WriteLine(cleanReply.Description);
            Console.WriteLine($"Material: {cleanReply.Amount}st {cleanReply.Commodity} Från {cleanReply.From_station} till {cleanReply.To_station}");
            Console.WriteLine($"Din belöning: {cleanReply.Reward_cr}cr, {cleanReply.Reward_reputation} ryktesökning");
            Console.WriteLine("\nVill du acceptera detta uppdraget? J - Ja/N - Nej eller tryck på 'x' för att återgå till menyn");

            string questResponse = Console.ReadLine().ToLower();
            if (questResponse == "j")
            {
                MissionHandler.AddMission(session, cleanReply);
            }
            else if (questResponse == "n")
            {
                Console.WriteLine("Du fortsätter leta efter uppdragsgivare...");
            }
            else
            {
                Console.WriteLine("Avbryter och återgår till stationsmenyn");
                    
            }
        }
    }
}
