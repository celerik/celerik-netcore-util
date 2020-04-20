using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Celerik.NetCore.Util.Test
{
    public class Actress
    {
        public string Name { get; set; }
        public int Rating { get; set; }
    }

    [TestClass]
    public class QueryUtilityTest : UtilBaseTest
    {
        private readonly List<Actress> _actresses = new List<Actress> {
            new Actress { Name = "Sasha Gray", Rating = 3 },
            new Actress { Name = "Nina Hartley", Rating = 4 },
            new Actress { Name = "Esperanza Gómez", Rating = 3 },
            new Actress { Name = "Mia Khalifa", Rating = 2 },
            new Actress { Name = "Savannah Shannon", Rating = 5 },
        };

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullQuery()
        {
            IQueryable<Actress> query = null;
            query.OrderBy("Name");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullPropName()
        {
            _actresses.AsQueryable().OrderBy(null);
        }

        [TestMethod]
        public void OrderBy()
        {
            var query = _actresses.AsQueryable().OrderBy("Name");
            var list = query.ToList();

            Assert.AreEqual("Esperanza Gómez", list[0].Name);
            Assert.AreEqual("Mia Khalifa", list[1].Name);
            Assert.AreEqual("Nina Hartley", list[2].Name);
            Assert.AreEqual("Sasha Gray", list[3].Name);
            Assert.AreEqual("Savannah Shannon", list[4].Name);
        }

        [TestMethod]
        public void OrderByDescending()
        {
            var query = _actresses.AsQueryable().OrderByDescending("Name");
            var list = query.ToList();

            Assert.AreEqual("Savannah Shannon", list[0].Name);
            Assert.AreEqual("Sasha Gray", list[1].Name);
            Assert.AreEqual("Nina Hartley", list[2].Name);
            Assert.AreEqual("Mia Khalifa", list[3].Name);
            Assert.AreEqual("Esperanza Gómez", list[4].Name);
        }

        [TestMethod]
        public void ThenBy()
        {
            var query = _actresses.AsQueryable().OrderByDescending("Rating").ThenBy("name");
            var list = query.ToList();

            Assert.AreEqual("Savannah Shannon", list[0].Name);
            Assert.AreEqual("Nina Hartley", list[1].Name);
            Assert.AreEqual("Esperanza Gómez", list[2].Name);
            Assert.AreEqual("Sasha Gray", list[3].Name);
            Assert.AreEqual("Mia Khalifa", list[4].Name);
        }

        [TestMethod]
        public void ThenByDescending()
        {
            var query = _actresses.AsQueryable().OrderByDescending("Rating").ThenByDescending("name");
            var list = query.ToList();

            Assert.AreEqual("Savannah Shannon", list[0].Name);
            Assert.AreEqual("Nina Hartley", list[1].Name);
            Assert.AreEqual("Sasha Gray", list[2].Name);
            Assert.AreEqual("Esperanza Gómez", list[3].Name);
            Assert.AreEqual("Mia Khalifa", list[4].Name);
        }
    }
}
