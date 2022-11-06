using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDrivenDevelopment.Tests
{
    [TestClass]
    public class TestForCollectionAsserts
    {
        public List<String> Users;
        [TestInitialize]
        public void Initialize()
        {
            Users = new List<string>();
            Users.Add("Sinem");
            Users.Add("Fatih");
            Users.Add("Defne");
        }
        [TestMethod]
        public void SameItemsAndOrders()
        {
            //elemanlar aynı ve sırası da aynı  olmalı
            List<String> localusers = new List<String>();
            localusers.Add("Sinem");
            localusers.Add("Fatih");
            localusers.Add("Defne");

            CollectionAssert.AreEqual(Users, localusers);
        }
        [TestMethod]
        public void SametItemButOrderDifferent()
        {
            //Elemanlar aynı olmalı sırası farklı olabilir.
            List<String> localusers = new List<String>();
            localusers.Add("Sinem");        
            localusers.Add("Defne");
            localusers.Add("Fatih");

            CollectionAssert.AreEquivalent(Users, localusers);
        }
        [TestMethod]
        public void CheckNullValue()
        {
            Users.Add(null);
            CollectionAssert.AllItemsAreNotNull(Users);
        }

        [TestMethod]
        public void ItemsUnique()
        {
            Users.Add("Fatih");
            CollectionAssert.AllItemsAreUnique(Users);
        }

        [TestMethod]
        public void ItemSameType()
        {
            ArrayList list = new ArrayList
            {
                "Fatih","Sinem",5
            };
            CollectionAssert.AllItemsAreInstancesOfType(list, typeof(String));
        }

        [TestMethod]
        public void ItemSubitemControl()
        {
            //karşılaştırılan liste verilen listedenin alt kümesi mi kontrolü yapar.
            List<String> newUsers = new List<string> { "Fatih","Sinem" };
            List<String> oldUsers = new List<string> { "Defne","Büşra"};

            CollectionAssert.IsSubsetOf(newUsers, Users);
            CollectionAssert.IsNotSubsetOf(oldUsers, Users);
        }

        [TestMethod]
        public void ItemContainsInList()
        {
            CollectionAssert.Contains(Users, "Sinem");
            CollectionAssert.DoesNotContain(Users, "Kadir"); 
        }
    }
}
