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
    public  class TestForClassInitializeAndCleanup
    {
        private static BasketManager manager;
        private static BasketItem item;

        [ClassInitialize]
        public static void TestInitialize(TestContext textContext)
        {
            manager = new BasketManager();
            item = new BasketItem
            {
                Product = new Product { Name = "Kalem", 
                                        Price = 1002, 
                                        ProductId = 1 },
                Quatity = 1

            };
            manager.AddBasket(item);
        }
        [TestMethod]
        public void AddBasketDiffrentItem()
        {
            int totalCount = manager.TotalQuantity;
            int totalItems = manager.TotalItems;

           
            manager.AddBasket(new BasketItem
            {
                Product = new Product
                {
                    Name = "Silgi",
                    Price = 2005,
                    ProductId = 2
                },
                Quatity = 1

            });

            Assert.AreEqual(totalCount + 1, manager.TotalQuantity);
            Assert.AreEqual(totalItems + 1, manager.TotalItems);

        }
        [TestMethod]
        [ExpectedException(typeof(Exception),AllowDerivedTypes =true)]
        public void AddBasketSameItem()
        {
            int totalCount = manager.TotalQuantity;
            int totalItems = manager.TotalItems;


            manager.AddBasket(new BasketItem
            {
                Product = new Product
                {
                    Name = "Kalem",
                    Price = 1002,
                    ProductId = 1
                },
                Quatity = 1

            });

            Assert.AreEqual(totalCount + 1, manager.TotalQuantity);
            Assert.AreEqual(totalItems , manager.TotalItems);
        }
        [ClassCleanup]
        public static void ClassCleanup()
        {
            manager.ClearBasket();
        }
    }
}
