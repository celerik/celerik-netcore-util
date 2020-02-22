using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Celerik.NetCore.Util.Test
{
    [TestClass]
    public class FirstTest
    {
        [TestMethod]
        public void TestAssert() => Assert.AreEqual(1, 1);
    }
}
