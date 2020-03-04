using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Celerik.NetCore.Util.Test
{
    [TestClass]
    public class FileUtilityTest : UtilBaseTest
    {
        [TestMethod]
        public void FileUtilityGetAssemblyDirectory()
        {
            var directory = FileUtility.GetAssemblyDirectory();
            Assert.AreNotEqual(null, directory);
        }
    }
}
