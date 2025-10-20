using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Between_Stars.Classes
{
    public abstract class CelestialBody
    {
        public string Id { get; set; }             // Unikt ID, bra för referenser i JSON
        public string Name { get; set; }           // Namn på planet, måne eller station
        public double X { get; set; }              // Koordinater för rymdkarta (kan vara AU eller nåt eget)
        public double Y { get; set; }
        public double Z { get; set; }
        public List<string> ConnectedBodies { get; set; } // ID:n på andra CelestialBodies som man kan resa till

        // Constructor
        protected CelestialBody()
        {
            ConnectedBodies = new List<string>();
        }
    }

}
