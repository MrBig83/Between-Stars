using Between_Stars.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Between_Stars.Utils
{
    public class MenuHelper
    {

        //public static bool ShowMainMenu(Player loggedInPlayer, MarketHandler marketHandler, List<CelestialBody> celestialBodies)//ApplicationManager applicationManager)
        public static bool ShowMainMenu(SessionData session)//ApplicationManager applicationManager)
        {
            Console.Clear();
            Console.WriteLine("\n" +
                "-- HUVUDMENY --\n");
            Console.WriteLine("1. Fortsätt"); //Välj karaktär om flera, annars kör på den som finns
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
                        //showSubMenu = HangarMenu(loggedInPlayer, marketHandler, celestialBodies);
                        while(showSubMenu)
                        {
                            showSubMenu = HangarMenu(session);
                        }
                    }
                    break;
                case "2":
                    Console.Clear();
                    //applicationManager.ShowAll();
                    break;
                case "3":
                    Console.Clear();
                    //applicationManager.ShowByStatus();
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
            
            Console.WriteLine($"================\n" +
                $"Du befinner dig i din hangar på " +
                $"{currentStation.Name}");
                Console.WriteLine("\n" +
                "-- HangarMeny --\n");
                Console.WriteLine("1. Kolla nyheter");
                Console.WriteLine("2. Visa marknad");
                Console.WriteLine("9. Avsluta");

                string inGameMenuChoice = Console.ReadLine();


                switch (inGameMenuChoice)
                {
                    case "1":
                        //Console.Clear();
                        Console.WriteLine($"Här ska AI få generera nyheter");
                        //IngameMenu(player);
                        break;
                    case "2":
                    Console.WriteLine($"Marknaden på {currentStation.Name}:\n");
                    MarketHandler.ShowMarket(session.Commodities);
                        //marketHandler.BuyCommodity(loggedInPlayer);
                        //Console.Clear();
                        //JsonHelper.SavePlayers("players.json", players); //Inte riktigt helt hundra här...
                        //Console.WriteLine($"Spelet är inte sparat just nu...");

                        //IngameMenu(player);
                        break;

                    case "9":
                        return false;
                    default:
                        Console.WriteLine("Vänligen ange ett korrekt menyalternativ");
                        break;

                }
            return true;
            
        }
        //public static bool ShowTradeMenu(SessionData session) //Denna skall visas i ShowMarket
        //{
        //    bool showTradeMenu = true;
        //}
      
    }
}