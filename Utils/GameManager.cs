using Between_Stars.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Between_Stars.Utils
{
    public class GameManager
    {
        private Player loggedInPlayer;
        private MarketHandler marketHandler;
        private List<Commodity> commodities;
        
        // Konstruktor som tar emot Player-objektet
        public GameManager(Player player, List<Commodity> commodities)
        {
            this.loggedInPlayer = player;
            this.commodities = commodities;
            marketHandler = new MarketHandler(commodities);
        }
        public void StartGame()
        {

            while (MenuHelper.ShowMainMenu(loggedInPlayer, marketHandler))
            {

            }


                //TEST
                //var marketHandler = new MarketHandler(commodities);
            //marketHandler.BuyCommodity(loggedInPlayer);
            // SLUT PÅ TEST

            //MainLoop();
        }

        //private void MainLoop()
        //{
        //    //Ladda JSON-filer i detta skede?
        //    bool running = true;
        //    while (running)
        //    {
        //        running = MenuHelper.ShowMainMenu(loggedInPlayer, marketHandler);
        //        //ShowMainMenu();
        //        //int choice = GetMenuChoice();
        //        //switch (choice)
        //        //{
        //        //    case 1: marketHandler.BuyCommodity(player); break;
        //        //    case 2: marketHandler.SellCommodity(player); break;
        //        //    case 3: TravelToAnotherMarket(); break;
        //        //    case 4: SaveGame(); break;
        //        //    case 5: running = false; break;
        //        //        // Osv
        //        //}
        //    }


        //}
        

  
    }
}
