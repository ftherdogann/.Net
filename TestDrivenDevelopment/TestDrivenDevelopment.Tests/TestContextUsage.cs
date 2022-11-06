using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDrivenDevelopment.Tests
{
    /// <summary>
    /// Summary description for TestContext
    /// </summary>
    [TestClass]
    public class TestContextUsage
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestContextInitialize()
        {
            TestContext.WriteLine("----TestContextInitialize----");
            TestContext.WriteLine("Test Adı:{0}", TestContext.TestName);
            TestContext.WriteLine("Test Durumu:{0}", TestContext.CurrentTestOutcome);
            TestContext.Properties["Parameter"] = "Sinem";
        }

        [TestMethod]
        public void TestContextTest()
        {
            TestContext.WriteLine("----TestContextTest----");
            TestContext.WriteLine("Test Adı:{0}",TestContext.TestName);
            TestContext.WriteLine("Test Durumu:{0}" , TestContext.CurrentTestOutcome);
            TestContext.WriteLine("Parametre:{0}" , TestContext.Properties["Parameter"]);
        }

        [TestCleanup]
        public void TestContextCleanup()
        {
            TestContext.WriteLine("----TestContextCleanup----");
            TestContext.WriteLine("Test Adı:{0}", TestContext.TestName);
            TestContext.WriteLine("Test Durumu:{0}" , TestContext.CurrentTestOutcome);
        }
    }
}
