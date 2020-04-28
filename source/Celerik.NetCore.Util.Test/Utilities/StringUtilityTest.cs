using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Celerik.NetCore.Util.Test
{
    [TestClass]
    public class StringUtilityTest
    {
        [TestMethod]
        public void FirstUpperRestLower()
        {
            Assert.AreEqual(null, ((string)null).FirstUpperRestLower());
            Assert.AreEqual(string.Empty, string.Empty.FirstUpperRestLower());
            Assert.AreEqual("A", "A".FirstUpperRestLower());
            Assert.AreEqual("A", "a".FirstUpperRestLower());
            Assert.AreEqual("Abc", "ABC".FirstUpperRestLower());
            Assert.AreEqual("Abc", "abc".FirstUpperRestLower());
        }
    }
}
