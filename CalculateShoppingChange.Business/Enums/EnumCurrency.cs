using System.ComponentModel;

namespace CalculateShoppingChange.Business.Enums
{
    public enum EnumCurrency
    {
        [Description("GBP")]
        GBP = 1,
        [Description("USD")]
        USD = 2,
        [Description("EUR")]
        EUR = 3,
        [Description("TRY")]
        TRY = 4
    }

}
