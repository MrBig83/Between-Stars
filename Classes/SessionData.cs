using Between_Stars.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Between_Stars.Classes
{
    public class SessionData
    {
        public Player LoggedInPlayer { get; set; }
        public List<CelestialBody> CelestialBodies { get; set; }
        public List<Commodity> Commodities { get; set; }
        public MarketHandler MarketHandler { get; set; }
        // Lägg till fler egenskaper vid behov!
        // T.ex. nuvarande station/planet, inloggad användare, kontoinställningar...
    }
}
