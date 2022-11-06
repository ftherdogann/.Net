using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDrivenDevelopment.Tests
{
    [TestClass]
    public class TestForStringAssert
    {
        [TestMethod]
        public void Contains()
        {
            StringAssert.Contains("Merhaba Sinem", "Sinem");

        }
        [TestMethod]
        public void Matches()
        {
            StringAssert.Matches("Merhaba Sinem", new System.Text.RegularExpressions.Regex(@"[a-zA-z]"));
            StringAssert.DoesNotMatch("Merhaba Sinem", new System.Text.RegularExpressions.Regex(@"[0-9]"));
        }
        [TestMethod]
        public void StartWith()
        {
            StringAssert.StartsWith("Sinem & Fatih Erdoğan", "Sinem");
        }
        [TestMethod]
        public void EndWith()
        {
            StringAssert.EndsWith("Sinem & Fatih Erdoğan", "Erdoğan");
        }
    }
}
