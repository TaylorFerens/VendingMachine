using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static VendingMachineApi.Classes.Enumerations;

namespace VendingMachineApi.Models
{
    public class Coin
    {
        public CoinType CoinType { get; set; }
        public int Value { get; set; }

        public int Quantity { get; set; }
    }
}
