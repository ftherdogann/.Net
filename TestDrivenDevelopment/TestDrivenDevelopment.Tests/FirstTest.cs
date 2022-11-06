using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDrivenDevelopment.Apps;
namespace TestDrivenDevelopment.Tests
{
    [TestClass]
    public class FirstTest
    {
        [TestMethod]
        public void ControlFactorial()
        {
            decimal expectedResult = 6;
            int input = 3;

            FirstAppClass mth = new FirstAppClass();
            decimal actualResult =mth.Factorial(input);

            Assert.AreEqual(expectedResult, actualResult);

        }

        [TestMethod]
        public void ControlCheckPrime()
        {
            bool expectedResult = true;

            int input = 11;

            FirstAppClass mth = new FirstAppClass();
            bool actualResult = mth.CheckPrime(input);

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
