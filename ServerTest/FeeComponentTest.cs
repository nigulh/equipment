using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.Model;

namespace ServerTest
{
    class MockCustomerRate : CustomerRate
    {
        public Currency Currency
        {
            get { return Currency.EUR; }
        }

        decimal CustomerRate.OneTimeFee
        {
            get { return 100; }
        }

        decimal CustomerRate.PremiumDailyFee
        {
            get { return 60; }
        }

        decimal CustomerRate.RegularDailyFee
        {
            get { return 40; }
        }
    }

    [TestClass]
    public class FeeComponentTest
    {
        MockCustomerRate rate;
        OneTimeFee oneTimeFee;
        PremiumFee premiumFee;

        [TestInitialize]
        public void Init()
        {
            rate = new MockCustomerRate();
            oneTimeFee = new OneTimeFee();
            premiumFee = new PremiumFee(2);
        }

        [TestMethod]
        public void CanApplyFees()
        {
            int daysLeft = 1;
            var ret = 0m;
            
            ret += oneTimeFee.ApplyFee(ref daysLeft, rate);
            ret += premiumFee.ApplyFee(ref daysLeft, rate);

            Assert.AreEqual(0, daysLeft);
            Assert.AreEqual(160, ret);
        }

        [TestMethod]
        public void EquipmentHasFee()
        {
            int daysLeft = 100;
            SpecializedEquipment equipment = new SpecializedEquipment();

            var price = equipment.CalculateRentalPrice(daysLeft, rate);

            Assert.AreEqual(new MoneyValue(4160m, Currency.EUR), price);
        }

        [TestMethod]
        public void EquipmentHasZeroPriceForNoDays()
        {
            int daysLeft = 0;
            SpecializedEquipment equipment = new SpecializedEquipment();

            var price = equipment.CalculateRentalPrice(daysLeft, rate);

            Assert.AreEqual(new MoneyValue(0m, Currency.EUR), price);
        }

        [TestMethod]
        public void EquipmentHasZeroPriceForNegativeDays()
        {
            int daysLeft = -10;
            SpecializedEquipment equipment = new SpecializedEquipment();

            var price = equipment.CalculateRentalPrice(daysLeft, rate);

            Assert.AreEqual(new MoneyValue(0m, Currency.EUR), price);
        }
    }
}
