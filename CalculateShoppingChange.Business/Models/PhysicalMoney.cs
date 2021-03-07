using CalculateShoppingChange.Business.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculateShoppingChange.Business.Models
{
    public class PhysicalMoney
    {
        public double Amount { get; set; }
        public int Count { get; set; }
        public virtual EnumCurrency Currency { get; set; }
    }
}
