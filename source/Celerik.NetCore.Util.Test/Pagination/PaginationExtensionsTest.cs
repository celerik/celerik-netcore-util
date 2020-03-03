﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Celerik.NetCore.Util.Test
{
    public class Cat { public string Name { get; set; } }

    [TestClass]
    public class PaginationExtensionsTest : UtilBaseTest
    {
        [TestMethod]
        public async Task Paginate()
        {
            var request = new PaginationRequest
            {
                PageNumber = 1,
                PageSize = 2,
                SortKey = "Name",
                SortDirection = "desc"
            };

            var items = new List<Cat> {
                new Cat { Name = "Cosme Felinito" },
                new Cat { Name = "Michi Naranjoso del Monte" },
                new Cat { Name = "Brad Michi" }
            }.AsQueryable();

            var pagination = await items.Paginate(request, isAsync: false);

            Assert.AreEqual(false, pagination.IsAscending);
            Assert.AreEqual(items.ElementAt(1).Name, pagination.Items.ElementAt(0).Name);
            Assert.AreEqual(items.ElementAt(0).Name, pagination.Items.ElementAt(1).Name);
            Assert.AreEqual(2, pagination.PageCount);
            Assert.AreEqual(1, pagination.PageNumber);
            Assert.AreEqual(2, pagination.PageSize);
            Assert.AreEqual(3, pagination.RecordCount);
            Assert.AreEqual("desc", pagination.SortDirection);
            Assert.AreEqual("Name", pagination.SortKey);
        }
    }
}
