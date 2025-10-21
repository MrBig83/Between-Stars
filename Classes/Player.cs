using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Between_Stars.Classes
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Credits { get; set; }
        public double Reputation { get; set; }
        public PlayerShip Ship { get; set; }
        public Dictionary<string, int> CurrentCargo { get; set; } = new(); // <varunamn, antal>
        public int CurrentLocationId { get; set; }

        public Player()
        {
            Credits = 1000; // Startpengar – sätt vad du vill
            Reputation = 0; // Startvärde
                            // Inventory = new List<Commodity>();
        }
    }
}
