using CalculateShoppingChange.Abstract;
using CalculateShoppingChange.Business.Concrete;
using CalculateShoppingChange.Business.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CalculateShoppingChange.Business.Tests
{
    [TestClass]
    public class ChangeManagerTests
    {
        // Test Cases
        // 1: After Payment, Change have to calculated correctly.
        // 2: Physical Money (Banknote and Coin) counts have to be calculated correctly according to currency type and change amount.
        // 3: Change parameters can't be less than zero.
        // 4: Users money have to be enough for payment (Payment cant be less than shopping cost.)
        // 5: Change parameters cant be more than hundred billion.

        [TestMethod]
        public void Change_Must_Be_Calculated_Accurately()
        {

            //Arrange
            IChangeService changeService = new ChangeManager();

            //Act
            Transaction transaction = new Transaction
            {
                GivenMoneyAmount = 200,
                ShoppingCostAmount = 37.28,
                Currency = Enums.EnumCurrency.GBP
            };
            var expected = transaction.GivenMoneyAmount - transaction.ShoppingCostAmount;
            var actual = changeService.GetChange(transaction);

            //Assert
            Assert.AreEqual(actual.Data.TotalChange, expected);
            Assert.AreEqual(actual.IsSuccess, true);
        }

        [TestMethod]
        public void Physical_Money_Counts_Have_To_Be_Calculated_Correctly()
        {
            //Arrange
            IChangeService changeService = new ChangeManager();

            //Act
            Transaction transaction = new Transaction
            {
                GivenMoneyAmount = 200,
                ShoppingCostAmount = 37.28,
                Currency = Enums.EnumCurrency.GBP
            };

            var expected = new ApiResponse<GetChangeResponse>() { Data = new GetChangeResponse() };
            expected.Data.PhysicalMoneyCounts = new Dictionary<string, int>() {
                { "50",   3 },
                { "10",   1 },
                { "2",    1 },
                { "0.5",  1 },
                { "0.2",  1 },
                { "0.02", 1 }
            };

            var actual = changeService.GetChange(transaction);

            //Assert
            Assert.AreEqual(expected.Data.PhysicalMoneyCounts.Count, actual.Data.PhysicalMoneyCounts.Count);
            CollectionAssert.AreEqual(
                expected.Data.PhysicalMoneyCounts, 
                actual.Data.PhysicalMoneyCounts
            );
            Assert.AreEqual(actual.IsSuccess, true);
        }

        [TestMethod]
        public void Change_Parameters_Cant_Be_Less_Than_Zero()
        {
            //Arrange
            IChangeService changeService = new ChangeManager();

            //Act
            Transaction transaction = new Transaction
            {
                GivenMoneyAmount = -100,
                ShoppingCostAmount = -22,
                Currency = Enums.EnumCurrency.GBP
            };

            var actual = changeService.GetChange(transaction);

            //Assert
            Assert.IsTrue(actual.Errors.Contains("Given money parameter can't be less than 0!"));
            Assert.IsTrue(actual.Errors.Contains("Shopping cost amount parameter can't be less than 0!"));
            Assert.AreEqual(actual.IsSuccess, false);
        }

        [TestMethod]
        public void Users_Money_Have_To_Be_Enough_For_Payment()
        {
            //Arrange
            IChangeService changeService = new ChangeManager();
            //Act
            Transaction transaction = new Transaction
            {
                GivenMoneyAmount = 50,
                ShoppingCostAmount = 120.5,
                Currency = Enums.EnumCurrency.GBP
            };
            var actual = changeService.GetChange(transaction);
            //Assert
            Assert.IsTrue(actual.Errors.Contains("Your money is not enough!"));
            Assert.AreEqual(actual.IsSuccess, false);
        }


        [TestMethod]
        public void Change_Parameters_Cant_Be_More_Than_Hundred_Billion()
        {
            //Arrange
            IChangeService changeService = new ChangeManager();
            //Act
            Transaction transaction = new Transaction
            {
                GivenMoneyAmount = 100000000001,
                ShoppingCostAmount = 100000000001,
                Currency = Enums.EnumCurrency.GBP
            };
            var actual = changeService.GetChange(transaction);
            //Assert
            Assert.IsTrue(actual.Errors.Contains("Can only handle money less than hundred billion!"));
            Assert.AreEqual(actual.IsSuccess, false);
        }


    }

}

