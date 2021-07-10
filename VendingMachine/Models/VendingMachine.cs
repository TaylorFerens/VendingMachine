using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static VendingMachineApi.Classes.Enumerations;

namespace VendingMachineApi.Models
{
    public class VendingMachine
    {
        public List<Drink> Drinks { get; set; }
        public List<Coin> Coins { get; set; }

        public VendingMachine()
        {
            this.Drinks = new List<Drink>();
            this.Drinks.Add(new Drink { Name = "Coke", Value = 25, Quantity = 5 });
            this.Drinks.Add(new Drink { Name = "Pepsi", Value = 36, Quantity = 15 });
            this.Drinks.Add(new Drink { Name = "Soda", Value = 45, Quantity = 3 });

            this.Coins = new List<Coin>();
            this.Coins.Add(new Coin { CoinType = CoinType.Penny, Quantity = 100, Value = 1 });
            this.Coins.Add(new Coin { CoinType = CoinType.Nickel, Quantity = 10, Value = 5 });
            this.Coins.Add(new Coin { CoinType = CoinType.Dime, Quantity = 5, Value = 10 });
            this.Coins.Add(new Coin { CoinType = CoinType.Quarter, Quantity = 25, Value = 25 });
        }
    }
}
