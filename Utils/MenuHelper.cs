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

        public static bool ShowMainMenu(Player loggedInPlayer, MarketHandler marketHandler)//ApplicationManager applicationManager)
        {
            Console.WriteLine("\n" +
                "-- HUVUDMENY --\n");
            Console.WriteLine("1. Fortsätt"); //Välj karaktär om flera, annars kör på den som finns
            Console.WriteLine("2. Skapa ny karaktär");
            Console.WriteLine("3. Visa alla karaktärer & status\n");

            Console.WriteLine("9. Avsluta");

            string menuChoice = Console.ReadLine();

            switch (menuChoice)
            {
                case "1":
                    Console.Clear();
                    IngameMenu(loggedInPlayer, marketHandler);
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

        public static void IngameMenu(Player loggedInPlayer, MarketHandler marketHandler)
        {


            Console.WriteLine("\n" +
            "-- Detta skall vara dynamiskt --\n");
            Console.WriteLine("1. Visa status");
            Console.WriteLine("2. Köp något"); //Fast det sparas ju automatiskt när något händer...
            Console.WriteLine("9. Avsluta");

            string inGameMenuChoice = Console.ReadLine();

            switch (inGameMenuChoice)
            {
                case "1":
                    //Console.Clear();
                    Console.WriteLine($"-X- sitter i ditt skepp:");
                    //IngameMenu(player);
                    break;
                case "2":
                    marketHandler.BuyCommodity(loggedInPlayer);
                    //Console.Clear();
                    //JsonHelper.SavePlayers("players.json", players); //Inte riktigt helt hundra här...
                    //Console.WriteLine($"Spelet är inte sparat just nu...");

                    //IngameMenu(player);
                    break;

                case "9":
                    break;
                default:
                    Console.WriteLine("Vänligen ange ett korrekt menyalternativ");
                    break;

            }

            //marketHandler = new MarketHandler();
            //MarketHandler.BuyCommodity(player, commodities);


            // Initiera player och marketHandler
            // Ladda save eller skapa ny
            // Initiera Player, antingen hårdkodat eller genom att fråga användaren


        }
    }
}