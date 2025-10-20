using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Between_Stars.Classes
{
    public class Planet : CelestialBody
    {
        public List<Moon> Moons { get; set; }           // Månar som kretsar kring planeten
        public List<SpaceStation> Stations { get; set; } // Stationer i omloppsbana

        public Planet()
        {
            Moons = new List<Moon>();
            Stations = new List<SpaceStation>();
        }
    }

}
