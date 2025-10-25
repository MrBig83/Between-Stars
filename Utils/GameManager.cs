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
        private SessionData session;

        public GameManager(SessionData session)
        {
            this.session = session;
        }
        
        public async Task StartGame()
        {
            

            MarketHandler.Restock(session); // =========== Körs på timer senare (Idle restock?)
            await MenuHelper.ShowMainMenu(session);
            // ...andra menyer och features...
        }
    }
}
