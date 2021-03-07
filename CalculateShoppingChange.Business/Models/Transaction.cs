using CalculateShoppingChange.Business.Enums;

namespace CalculateShoppingChange.Business
{
    public class Transaction
    {
        public double GivenMoneyAmount { get; set; }
        public double ShoppingCostAmount { get; set; }
        public virtual EnumCurrency Currency { get; set; }
    }
}
