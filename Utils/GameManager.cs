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
        
        public bool StartGame()
        {
            return MenuHelper.ShowMainMenu(session);
            // ...andra menyer och features...
        }
    }
}
