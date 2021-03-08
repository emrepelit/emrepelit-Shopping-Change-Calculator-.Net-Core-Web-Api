using CalculateShoppingChange.Abstract;
using CalculateShoppingChange.Business.Enums;
using CalculateShoppingChange.Business.Helper;
using CalculateShoppingChange.Business.Models;
using System;
using System.Collections.Generic;

namespace CalculateShoppingChange.Business.Concrete
{
    public class ChangeManager : IChangeService
    {
        
        public ApiResponse<GetChangeResponse> GetChange(Transaction transaction)
        {
            var response = new ApiResponse<GetChangeResponse>();

            ValidateGivenMoney(transaction, response);
            ValidateShoppingCost(transaction, response);
            if (transaction.ShoppingCostAmount > transaction.GivenMoneyAmount)
            {
                response.Errors.Add("Your money is not enough!");
            }
            if (response.IsSuccess)
            {
                response.Data = CalculateChange(transaction);
            }

            return response;
        }

       

        private GetChangeResponse CalculateChange(Transaction transaction)
        {
            double remainingChange = Math.Round(transaction.GivenMoneyAmount - transaction.ShoppingCostAmount, 2);
            var result = new GetChangeResponse
            {
                TotalChange = remainingChange
            };

            var currencyList = PrepareBanknotesByCurrency(transaction.Currency);

            foreach (var banknotVal in currencyList)
            {
                var temp = CalculateBanknoteCounts(remainingChange, banknotVal);

                if (temp.Count > 0)
                {
                    remainingChange = Math.Round(remainingChange - (banknotVal * Convert.ToDouble(temp.Count)), 2);
                    result.PhysicalMoneyCounts.Add(
                        temp.Amount.ToString(System.Globalization.CultureInfo.InvariantCulture),
                        temp.Count
                    );
                }
            }

            return result;
        }

        private PhysicalMoney CalculateBanknoteCounts(double remainingChangeAmount, double banknotVal)
        {
            var response = new PhysicalMoney();
            int times = (int)(remainingChangeAmount / banknotVal);

            if (times == 0)
            {
                return response;
            }

            response.Count = times;
            response.Amount = banknotVal;

            return response;
        }

        private List<double> PrepareBanknotesByCurrency(EnumCurrency currency)
        {
            return currency switch
            {
                EnumCurrency.GBP => CurrencyHelper.SterlingDenominationList,
                EnumCurrency.USD => CurrencyHelper.DolarDenominationList,
                EnumCurrency.EUR => CurrencyHelper.EuroDenominationList,
                EnumCurrency.TRY => CurrencyHelper.TurkishLiraDenominationList,
                _ => CurrencyHelper.TurkishLiraDenominationList,
            };
        }
        private void ValidateShoppingCost(Transaction transaction, ApiResponse<GetChangeResponse> response)
        {
            if (transaction.ShoppingCostAmount < 0)
            {
                response.Errors.Add("Shopping cost amount parameter can't be less than 0!");
            }
            if (transaction.ShoppingCostAmount > 100000000000)
            {
                response.Errors.Add("Can only handle money less than hundred billion!");
            }
        }

        private void ValidateGivenMoney(Transaction transaction, ApiResponse<GetChangeResponse> response)
        {
            if (transaction.GivenMoneyAmount < 0)
            {
                response.Errors.Add("Given money parameter can't be less than 0!");
            }
            if (transaction.GivenMoneyAmount > 100000000000)
            {
                response.Errors.Add("Can only handle money less than hundred billion!");
            }
        }


    }
}
