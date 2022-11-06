using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TestDrivenDevelopment.Tests
{
    /// <summary>
    /// Summary description for TestAttributes
    /// </summary>
    [TestClass]
    public class TestAttributes
    {
      

        [TestMethod]
        [Owner("Fatih")] //Sahipliği
        [TestCategory("Developer")]//Categoriye ayırmak için
        [Priority(1)]//Öncelik ataması
        [TestProperty("Update By","Sinem")]//Ek test özellikleri için
        [Timeout(1000)] //Timeout süresi belirtiyoruz.
        [Description("Açıklama giriniz.")]
        public void TestMethod1()
        {
            Thread.Sleep(1500);//Timeout süresini aştığımızda fail olduğunu görmek için
            Assert.AreEqual(1, 1);
        }

        [TestMethod]
        [Owner("Sinem")]
        [TestCategory("Developer")]
        [TestCategory("Tester")]
        [Priority(2)]
        [TestProperty("Update By", "Fatih")]
        [TestProperty("Update By2", "Sinem")]
        public void TestMethod2()
        {
            Assert.AreEqual(2, 2);
        }

        [TestMethod]
        [Ignore]//Test metodunu atlamak için kullanılır.
        [Owner("Sinem")]
        [TestCategory("Tester")]
        [Priority(2)]
        [TestProperty("Update By", "Fatih")]
        public void TestMethod3()
        {
            Assert.AreEqual(3, 3);
        }
    }
}
