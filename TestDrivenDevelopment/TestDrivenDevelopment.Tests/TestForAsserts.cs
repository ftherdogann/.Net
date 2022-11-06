using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDrivenDevelopment.Tests
{
    [TestClass]
    public class TestForAsserts
    {
        [TestMethod]
        public void CalculateSqrt()
        {
            const double input = 16;
            const double expected = 4;
            double actual = Math.Sqrt(input);

            Assert.AreEqual(expected, actual,"{0} sayısının karekökü {1} olmalıdır.",input,expected);

        }

        [TestMethod]
        public void CalculateSqrtWithDelta()
        {
            const double input = 10;
            const double expected = 3.1622;
            double delta = 0.0001;
            double actual = Math.Sqrt(input);

            Assert.AreEqual(expected, actual, delta);
        }

        [TestMethod]
        public void StringIsEqual()
        {
            string expected = "Fatih";
            string actual = "fatih";

            Assert.AreEqual(expected, actual, true);
        }

        [TestMethod]
        public void ValueNotEqual()
        {
            const double expected = 0;
            double actual = Math.Pow(5, 0);

            Assert.AreNotEqual(expected, actual);
        }

        [TestMethod]
        public void AreSame()
        {
            //Same Refranslarının aynı olup olmadığına bakar
            var numbers = new byte[] { 1, 2, 3, 4 };
            var otherNumbers = numbers;
            numbers[0] = 4;

            Assert.AreSame(numbers, otherNumbers);

           
        }
        [TestMethod]
        public void NotSame()
        {
            //Same Refranslarının aynı olup olmadığına bakar
            int a = 5;
            int b = a;
            Assert.AreNotSame(a, b,"İki sayı birbirinden farklı referanslara ait");
        }

        [TestMethod]
        public void Inconclusive()
        {
            //Iyarı amaçlı kullanılan metotdur.
            //Testin başarılı ama yeterli olmadığını ifade eder.

            Assert.AreEqual(1, 1);
            Assert.Inconclusive("Bu test için yeterli değil.Diğer testlerle doğrulayın");
        }

        [TestMethod]
        public void IsInstanceOfType()
        {
            var number = 5m;

            Assert.IsInstanceOfType(number, typeof(decimal));
            Assert.IsNotInstanceOfType(number, typeof(int));
        }

        [TestMethod]
        public void IsTrueOrFalse()
        {

            Assert.IsTrue(10%2==0);
            Assert.IsFalse(10 % 2 == 1);
        }

        [TestMethod]
        public void IsNull()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

            var firstNumber = numbers.FirstOrDefault(x => x > 5);
            var secondNumber = numbers.FirstOrDefault(x => x > 4);

            Assert.IsNull(firstNumber, "IsNull İşlemi başarısız");
            Assert.IsNotNull(secondNumber, "IsNull İşlemi başarısız");
        }

        [TestMethod]
        public void Fail()
        {
            try
            {
                int firstNumber = 5;
                var result = firstNumber / 0;
            }
            catch (Exception)
            {

                Assert.Fail("Test hata aldı başarısız oldu ");
            }
        }
    }
}
