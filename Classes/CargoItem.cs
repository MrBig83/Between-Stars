using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Between_Stars.Classes
{
    public class CargoItem
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public string Acronym { get; set; }
        public double Volume { get; set; }
        // Lägg gärna till fler properties senare (t.ex. BasePrice, Volume, osv)

        public CargoItem() { }
        public CargoItem(string name, int amount, string acronym, double volume)
        {
            Name = name;
            Amount = amount;
            Acronym = acronym;
            Volume = volume;
        }
    }

}
