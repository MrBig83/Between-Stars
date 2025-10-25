using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Between_Stars.Classes
{
    public class CelestialBody
    {
        public int Id { get; set; }             // Unikt ID, bra för referenser i JSON
        public string Name { get; set; }           // Namn på planet, måne eller station
        public Position Position { get; set; }              // Koordinater för rymdkarta (kan vara AU eller nåt eget)
        public List<InventoryEntry> Inventory { get; set; }  // Varor stationen säljer/köper
        public List<string> ConnectedBodies { get; set; } // ID:n på andra CelestialBodies som man kan resa till

        // Constructor
        // Tom konstruktor för deserialisering
        public CelestialBody()
        {
            Inventory = new List<InventoryEntry>();
            ConnectedBodies = new List<string>();
        }
        public CelestialBody(int id, string name, int x, int y, int z,
            List<InventoryEntry> inventory = null, List<string> connectedBodies = null)
        {
            Id = id;
            Name = name;
            Position = new Position { X = x, Y = y, Z = z };
            Inventory = inventory ?? new List<InventoryEntry>();
            ConnectedBodies = connectedBodies ?? new List<string>();
        }
    }

}
