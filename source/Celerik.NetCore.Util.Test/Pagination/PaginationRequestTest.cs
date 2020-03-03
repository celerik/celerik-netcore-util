using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Celerik.NetCore.Util.Test
{
    [TestClass]
    public class PaginationRequestTest : UtilBaseTest
    {
        [TestMethod]
        public void SortAscending()
        {
            var request = new PaginationRequest
            {
                PageNumber = 1,
                PageSize = 20,
                SortKey = "Name",
                SortDirection = "asc"
            };

            Assert.AreEqual(true, request.IsAscending);
        }

        [TestMethod]
        public void SortDescending()
        {
            var request = new PaginationRequest
            {
                PageNumber = 1,
                PageSize = 20,
                SortKey = "Name",
                SortDirection = "desc"
            };

            Assert.AreEqual(false, request.IsAscending);
        }

        [TestMethod]
        public void DefaultSort()
        {
            var request = new PaginationRequest
            {
                PageNumber = 1,
                PageSize = 20,
                SortKey = "Name"
            };

            Assert.AreEqual(true, request.IsAscending);
        }
    }
}
