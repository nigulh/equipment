using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.Model;

namespace ServerTest
{
    [TestClass]
    public class ValueTest
    {
        MoneyValue one;
        MoneyValue two;
        MoneyValue otherOne;

        [TestInitialize]
        public void Init()
        {
            one = new MoneyValue(1, Currency.EUR);
            two = new MoneyValue(2, Currency.EUR);
            otherOne = new MoneyValue(1, Currency.USD);
        }

        [TestMethod]
        public void OnePlusOneShouldEqualTwo()
        {
            MoneyValue onePlusOne = one + one;
            Assert.AreEqual(onePlusOne, two);
        }

        [TestMethod]
        public void TwoMinusOneShouldEqualOne()
        {
            MoneyValue twoMinusOne = two - one;
            Assert.AreEqual(twoMinusOne, one);
        }

        [TestMethod]
        public void DoubleOneShouldEqualTwo()
        {
            MoneyValue oneDouble = one * 2;
            MoneyValue doubleOne = 2 * one;
            Assert.AreEqual(two, oneDouble);
            Assert.AreEqual(two, doubleOne);
        }

        [TestMethod]
        public void OneShouldNotEqualTwo()
        {
            Assert.AreNotEqual(one, two);
        }

        [TestMethod]
        public void OneShouldNotEqualOtherOne()
        {
            Assert.AreNotEqual(one, otherOne);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CannotAddWithOtherCurrency()
        {
            var x = one + otherOne;
        }
    }
}
