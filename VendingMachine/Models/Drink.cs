using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VendingMachineApi.Models
{
    public class Drink
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public int Quantity { get; set; }
        public int Order { get; set; }
    }
}
