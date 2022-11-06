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
    public class TestForTestInitializeAndCleanup
    {
        private BasketManager manager;
        private BasketItem item;
        [TestInitialize]
        public void TestInitialize()
        {
            manager = new BasketManager();
            item = new BasketItem
            {
                Product = new Product { Name = "Kalem", Price = 1002, ProductId = 1 },
                Quatity = 1

            };
            manager.AddBasket(item);
        }

        [TestMethod]
        public void BasketItemAddTest()
        {
            int expected = 1;

            int actual = manager.TotalItems;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BasketItemRemoveTest()
        {
            int expected = manager.TotalItems;

            manager.RemoveBasket(1);

            int actual = manager.TotalItems;

            Assert.AreEqual(expected-1,actual);
        }

        [TestMethod]
        public void CleanBasketListTest()
        {
            manager.ClearBasket();

            Assert.AreEqual(0, manager.TotalItems);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            manager.ClearBasket();
        }
    }
}
