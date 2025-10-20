using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Between_Stars.Classes
{
    public class Player
    {
        public string Name { get; set; }
        public double Credits { get; set; }
        public double Reputation { get; set; }
        public PlayerShip Ship { get; set; }
        // public List<string> OwnedStations { get; set; }
        // public List<Commodity> Inventory { get; set; }
        // osv...

        // Constructor (valfritt)
        public Player()
        {
            Credits = 1000; // Startpengar – sätt vad du vill
            Reputation = 0; // Startvärde
                            // Inventory = new List<Commodity>();
        }
    }

}
