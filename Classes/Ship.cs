using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Between_Stars.Classes
{
    public class Ship
    {
        public int ShipId { get; set; }
        public string Name { get; set; }
        public int FuelCapacity { get; set; }
        public int CargoCapacity { get; set; }
        public int Tier { get; set; } 






        public void DisplayInfo()
        {
            Console.WriteLine("Visa info om skeppet");
        }

        public void Attack()
        {
            Console.WriteLine("Attackera");
        }

    }
}
