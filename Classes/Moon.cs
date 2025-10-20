using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Between_Stars.Classes
{
    public class Moon : CelestialBody
    {
        public string ParentPlanetId { get; set; }     // Referens till planeten månen tillhör
        public List<SpaceStation> Stations { get; set; }

        public Moon()
        {
            Stations = new List<SpaceStation>();
        }
    }

}
