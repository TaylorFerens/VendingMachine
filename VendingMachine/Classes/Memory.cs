using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendingMachineApi.Models;

namespace VendingMachineApi.Classes
{
    public static class TempMemory
    {
        public static VendingMachine VendingMachine = new VendingMachine();
    }
}
