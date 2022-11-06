using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TestDrivenDevelopment.Apps;

namespace TestDrivenDevelopment.Tests
{
    /// <summary>
    /// Summary description for TestForDataDrivenUnitTest
    /// </summary>
    [TestClass]
    public class TestForDataDrivenUnitTest
    {

        public TestContext TestContext { get; set; }

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", 
            "Users.xml",
            "user",DataAccessMethod.Sequential)]
        [TestMethod]
        public void DataDrivenTest()
        {

            //Biz xml deki verileri okuyup testleri yapılıyor. Bunu db seviyesinde de yapabiliriz.
            var manager = new AddUser();

            string name = TestContext.DataRow["name"].ToString();
            string phone = TestContext.DataRow["phoneNumber"].ToString();
            string email = TestContext.DataRow["email"].ToString();

            var result = manager.UserAdd(name,phone, email);

            Assert.IsTrue(result);

        }
    }
}
