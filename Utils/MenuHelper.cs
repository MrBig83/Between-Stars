using Between_Stars.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Between_Stars.Utils
{
    public class MenuHelper
    {
        public static async Task ShowMainMenu(SessionData session)
        {
            bool gameRunning = true;
            while(gameRunning)
            {

            
            Console.Clear();
            Console.WriteLine("\n" +
                "-- HUVUDMENY --\n");
            Console.WriteLine("1. Fortsätt"); 
            Console.WriteLine("2. Skapa ny karaktär");
            Console.WriteLine("3. Visa alla karaktärer & status\n");

            Console.WriteLine("9. Avsluta");

            string menuChoice = Console.ReadLine();

            switch (menuChoice)
            {
                case "1":
                    Console.Clear();
                    if(session.LoggedInPlayer.CurrentLocationId == 0)
                    {
                        //Här ska vi visa rymd- / resemenyn
                    }
                    else
                    {
                        await HangarMenu(session);
                    }
                    break;
                case "2":
                    Console.Clear();
                    break;
                case "3":
                    Console.Clear();
                    break;
                case "9":
                    gameRunning =  false;
                    break;
                default:
                    Console.WriteLine("Vänligen ange ett korrekt menyalternativ");
                    break;

            }
            
            }
        }

        public static async Task HangarMenu(SessionData session)
        {
            bool showHangarMenu = true;
            while (showHangarMenu)
            {

                var currentStation = session.CelestialBodies.First(s => s.Id == session.LoggedInPlayer.CurrentLocationId);

                //Console.WriteLine($"Du befinner dig i din hangar på " +
                //$"{currentStation.Name}");
                Console.WriteLine("\n" +
                "-- HangarMeny --\n");
                Console.WriteLine("1. Kontrollera nyheter");
                Console.WriteLine("2. Visa Skeppstatus");
                Console.WriteLine("3. Besök handelskammaren");
                Console.WriteLine("4. Handla varor");
                Console.WriteLine("5. Sälja varor");
                Console.WriteLine("6. Besök pubben");
                Console.WriteLine("7. Köp marknadsanalys");
                Console.WriteLine("8. Lämna stationen");
                Console.WriteLine("9. Avsluta");

                string inGameMenuChoice = Console.ReadLine();
                switch (inGameMenuChoice)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine($"Här ska AI få generera nyheter");

                        break;
                    case "2":
                        Console.Clear();

                        double cargoVolume = 0;
                        foreach (var item in session.LoggedInPlayer.Cargo)
                        {
                            cargoVolume = cargoVolume + item.Amount * item.Volume;
                        }



                        Console.WriteLine($"Ditt skepp: {session.Ships.FirstOrDefault(s => s.ShipId == session.LoggedInPlayer.ShipId).Name}\n" +
                            $"Nuvarande bränslenivå: {session.LoggedInPlayer.CurrentFuel}/{session.LoggedInPlayer.FuelCapacity}\n" +
                            $"Nuvarande lastmängd: {cargoVolume}/{session.LoggedInPlayer.CargoCapacity}\n");
                        int index = 1;
                        foreach (var item in session.LoggedInPlayer.Cargo)
                        {
                            Console.WriteLine($"{index++}. {item.Name} - {item.Amount} st ({session.Commodities.FirstOrDefault(c => c.Name == item.Name).Volume * item.Amount} m³)");
                        }

                        break;
                    case "3":
                        Console.Clear();
                        await TradeMenu(session);
                        //MarketHandler.ShowMarket(session);

                        break;
                    case "4":
                        //Console.Clear();
                        //MarketHandler.BuyCommodity(session);

                        break;
                    case "5":
                        //Console.Clear();
                        //Console.WriteLine($"Marknaden på {currentStation.Name}:\n");
                        //MarketHandler.SellCommodity(session);

                        break;
                    case "6":
                        Console.Clear();
                        Console.WriteLine($"Du besöker pubben...\n");
                        await APIHandler.VisaMeny();
                        break;
                    case "7":
                        Console.Clear();
                        int analysisPrice = MarketHandler.GetAnalysisPrice(session);
                        Console.WriteLine($"Vill du köpa en Marknadsanalys för {analysisPrice}cr? (J - Ja / N - Nej)");
                        string userInput = Console.ReadLine().ToLower();
                        if (userInput == "j")
                        {
                            Console.Clear();
                            Console.WriteLine($"Du köper en marknadsanalys och ser alla priser på alla stationer i sektorn\n");
                            session.LoggedInPlayer.Credits -= analysisPrice;
                            MarketHandler.MarketAnalysis(session);

                        }
                        Console.Clear();
                        break;
                    case "8":
                        Console.Clear();
                        TravelHandler.DisplayTravelDestinations(session);
                        break;

                    case "9":
                        showHangarMenu = false;
                        break;
                    default:
                        Console.WriteLine("Vänligen ange ett korrekt menyalternativ");
                        break;

                }
                
            }
        }
        public static async Task TradeMenu(SessionData session)

        {
            var currentStation = session.CelestialBodies.First(s => s.Id == session.LoggedInPlayer.CurrentLocationId);
            Console.WriteLine("-- Välkommen till handelskammaren --\n");
            Console.WriteLine("1. Handla varor");
            Console.WriteLine("2. Sälja varor");
            Console.WriteLine("3. Tillbaka till hangaren");

            string tradeMenuInput = Console.ReadLine().ToLower();

            switch(tradeMenuInput)
            {
                case "1":
                    Console.Clear();
                    MarketHandler.BuyCommodity(session);
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine($"Marknaden på {currentStation.Name}:\n");
                    MarketHandler.SellCommodity(session);
                    break;
                default:
                    break;

            }
                

        }

      
    }
}