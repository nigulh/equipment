using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ServerTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void UnitTestShouldRun()
        {
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void UnitTestShouldFail()
        {
            //Assert.IsTrue(false);
        }
    }
}
