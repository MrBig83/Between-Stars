using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Between_Stars.Classes
{
    public class Commodity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public double Volume { get; set; } // Hur mycket plats en vara tar (m³)
                                           // Vill du kan du lägga till fler attribut som ID, kategori, etc.

        public Commodity() { }

        public Commodity(string name, double price, double volume)
        {
            Name = name;
            Price = price;
            Volume = volume;
            

        }
    }
}
