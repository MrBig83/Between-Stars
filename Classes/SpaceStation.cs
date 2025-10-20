using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Between_Stars.Classes
{
    public class SpaceStation : CelestialBody
    {
        public string OrbitingBodyId { get; set; }     // Kan vara planet eller måne
        public bool CanRefuel { get; set; }            // Tankning möjlig här?
        public List<string> MarketCommodities { get; set; } // Varor som säljs här

        public SpaceStation()
        {
            MarketCommodities = new List<string>();
        }
    }

}
