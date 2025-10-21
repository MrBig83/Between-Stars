using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Between_Stars.Classes
{
    public class Commodity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Acronym { get; set; }
        public double BasePrice { get; set; }
        public double Volume { get; set; } // Hur mycket plats en vara tar (m³)
                                           // Vill du kan du lägga till fler attribut som ID, kategori, etc.

        public Commodity() { }

        public Commodity(int id, string name, string acronym, double baseprice, double volume)
        {
            Id = id;
            Name = name;
            Acronym = acronym;
            BasePrice = baseprice;
            Volume = volume;
            

        }
    }
}
