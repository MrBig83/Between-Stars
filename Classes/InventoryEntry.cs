using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Between_Stars.Classes
{
    public class InventoryEntry
    {
        public int CommodityId { get; set; }   // Referens till Commodity i huvudboken
        public int Stock { get; set; }         // Hur många finns tillgängligt
        public double Price { get; set; }      // Det lokala priset på stationen (kan variera)
    }

}
