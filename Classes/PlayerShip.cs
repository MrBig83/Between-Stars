using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Between_Stars.Classes
{
    public class PlayerShip : Ship
    {
        
        public int FuelCapacity { get; set; }
        public double CurrentFuel { get; set; }
        

        public int CargoCapacity { get; set; }
        public double CurrentCargo { get; set; }
        //public int Price { get; set; }

        //public PlayerShip(string name, int tier, int fuelCapacity, double currentFuel, int cargoCapacity, int currentCargo, int price)
        //{
        //    Name = name;
        //    Tier = tier;
        //    FuelCapacity = fuelCapacity;
        //    CurrentFuel = currentFuel;
        //    CargoCapacity = cargoCapacity;
        //    CurrentCargo = currentCargo;
        //    Price = price;

        //}

        public void Buy()
        {
            Console.WriteLine("Köp varor");
            //TradeItem & Amount
        }

        public void Sell()
        {
            Console.WriteLine("Sälj varor");
            //TradItem & Amount
        }
    }
}
//"Name": "Starter",
//    "FuelCapacity": 5,
//    "CurrentFuel": 5,
//    "Tier": 1