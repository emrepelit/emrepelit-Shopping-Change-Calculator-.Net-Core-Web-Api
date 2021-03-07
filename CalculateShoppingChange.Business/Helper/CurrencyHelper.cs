using CalculateShoppingChange.Business.Enums;
using System.Collections.Generic;

namespace CalculateShoppingChange.Business.Helper
{
    public class CurrencyHelper
    {
        public readonly static List<double> SterlingDenominationList = new List<double>() { 50, 20, 10, 5, 2, 1, 0.5, 0.2, 0.1, 0.05, 0.02, 0.01 };
        public readonly static List<double> DolarDenominationList = new List<double>() { 100, 50, 20, 10, 5, 1, 0.5, 0.25, 0.10, 0.05, 0.01 };
        public readonly static List<double> EuroDenominationList = new List<double>() { 200, 100, 50, 20, 10, 5, 2, 1, 0.5, 0.25, 0.1, 0.05, 0.02, 0.01 };
        public readonly static List<double> TurkishLiraDenominationList = new List<double>() { 200, 100, 50, 20, 10, 5, 1, 0.5, 0.25, 0.10, 0.05, 0.01 };
    }
}
