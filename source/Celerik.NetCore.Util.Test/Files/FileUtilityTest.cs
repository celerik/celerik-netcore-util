using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Celerik.NetCore.Util.Test
{
    [TestClass]
    public class FileUtilityTest
    {
        [TestMethod]
        public void GetExecutingAssemblyDir()
        {
            var directory = FileUtility.GetExecutingAssemblyDir();
            Assert.AreNotEqual(null, directory);
        }
    }
}
