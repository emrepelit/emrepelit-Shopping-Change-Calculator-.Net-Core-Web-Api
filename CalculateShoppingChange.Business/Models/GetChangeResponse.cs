using CalculateShoppingChange.Business.Enums;
using System.Collections.Generic;

namespace CalculateShoppingChange.Business.Models
{
    public class GetChangeResponse
    {
        public double TotalChange { get; set; }
        public Dictionary<string, int> PhysicalMoneyCounts { get; set; } = new Dictionary<string, int>();

    }
}
