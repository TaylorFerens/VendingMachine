using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VendingMachineApi.Models
{
    public class Order
    {
        public List<Coin> Coins { get; set; }
        public List<Drink> Drinks {get;set;}
    }
}
