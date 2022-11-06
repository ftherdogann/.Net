using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDrivenDevelopment.Tests
{
    [TestClass]
    public class TestForAssemblyInitializeAndCleanup
    {
        //Assembly level tüm testleri kapsayacak şekilde bir defa çalışır.
        [AssemblyInitialize]
        public static void  AllTestOneRunStart(TestContext testContext)
        {
            Debug.WriteLine("assembly Initialize: Testler başladı.");
        }

        [TestMethod]
        public void AssebblyLevelTest()
        {
            Debug.WriteLine("assembly metot çalıştı");
        }

        [AssemblyCleanup]
        public static void AllTestOneRunEnd()
        {
            Debug.WriteLine("assembly Cleanup: Testler bitti.");
        }
    }
}
