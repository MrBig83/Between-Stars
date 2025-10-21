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

        // Skeppsinfo
        public int ShipId { get; set; }
        public int Tier { get; set; }
        public int CargoCapacity { get; set; }
        public double CurrentCargo { get; set; }
        public int FuelCapacity { get; set; }
        public int CurrentFuel { get; set; }
        public int ShieldLevel { get; set; }
        public int EngineLevel { get; set; }
        // ...lägg till fler komponenter

        public Dictionary<string, int> Cargo { get; set; } = new();
        public int CurrentLocationId { get; set; }
    }
}
