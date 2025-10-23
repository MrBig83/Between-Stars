using Between_Stars.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Between_Stars.Utils
{
    public class MenuHelper
    {
        public static bool ShowMainMenu(SessionData session)
        {
            Console.Clear();
            Console.WriteLine("\n" +
                "-- HUVUDMENY --\n");
            Console.WriteLine("1. Fortsätt"); 
            Console.WriteLine("2. Skapa ny karaktär");
            Console.WriteLine("3. Visa alla karaktärer & status\n");

            Console.WriteLine("9. Avsluta");

            string menuChoice = Console.ReadLine();
            bool showSubMenu = true;

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
                        while(showSubMenu)
                        {
                            showSubMenu = HangarMenu(session);
                        }
                    }
                    break;
                case "2":
                    Console.Clear();
                    break;
                case "3":
                    Console.Clear();
                    break;
                case "9":
                    return false;
                default:
                    Console.WriteLine("Vänligen ange ett korrekt menyalternativ");
                    break;

            }
            return true;
        }

        public static bool HangarMenu(SessionData session)
        {
            var currentStation = session.CelestialBodies.First(s => s.Id == session.LoggedInPlayer.CurrentLocationId);
            bool showIngameMenu = true;
            
            //Console.WriteLine($"Du befinner dig i din hangar på " +
            //$"{currentStation.Name}");
            Console.WriteLine("\n" +
            "-- HangarMeny --\n");
            Console.WriteLine("1. Kontrollera nyheter");
            Console.WriteLine("2. Visa Skeppstatus");
            Console.WriteLine("3. Visa marknad");
            Console.WriteLine("4. Handla varor");
            Console.WriteLine("5. Sälja varor");
            Console.WriteLine("6. Besök pubben");
            Console.WriteLine("7. Köp marknadsanalys");
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
                    Console.WriteLine($"Ditt skepp: {session.Ships.FirstOrDefault(s => s.ShipId == session.LoggedInPlayer.ShipId).Name}\n" +
                        $"Nuvarande bränslenivå: {session.LoggedInPlayer.CurrentFuel}/{session.LoggedInPlayer.FuelCapacity}\n" +
                        $"Nuvarande lastmängd: {session.LoggedInPlayer.CurrentCargo}/{session.LoggedInPlayer.CargoCapacity}\n");
                        int index = 1;
                        foreach (var item in session.LoggedInPlayer.Cargo)
                        {
                            Console.WriteLine($"{index++}. {item.Name} - {item.Amount} st ({session.Commodities.FirstOrDefault(c => c.Name == item.Name).Volume * item.Amount} m³)");
                        }

                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine($"Marknaden på {currentStation.Name}:\n");
                    MarketHandler.ShowMarket(session);

                    break;
                case "4":
                    Console.Clear();
                    MarketHandler.BuyCommodity(session);

                    break;
                case "5":
                    Console.Clear();
                    Console.WriteLine($"Marknaden på {currentStation.Name}:\n");
                    MarketHandler.SellCommodity(session);

                    break;
                case "6":
                    Console.Clear();
                    Console.WriteLine($"Du besöker pubben...\n");
                    break;
                case "7":
                    Console.Clear();
                    Console.WriteLine($"Du köper en marknadsanalys och ser alla priser på alla stationer i sektorn\n");
                    break;

                case "9":
                    return false;
                default:
                    Console.WriteLine("Vänligen ange ett korrekt menyalternativ");
                    break;

            }
        return true;
            
        }

      
    }
}