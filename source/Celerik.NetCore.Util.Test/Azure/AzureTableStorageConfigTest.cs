using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Celerik.NetCore.Util.Test
{
    [TestClass]
    public class AzureTableStorageConfigTest
    {
        [TestMethod]
        public void Constructor()
        {
            var config = new AzureTableStorageConfig
            {
                ConnectionString = "DummyConnectionString",
                HideExceptions = true,
                TableName = "Errors"
            };

            Assert.AreEqual("DummyConnectionString", config.ConnectionString);
            Assert.AreEqual(true, config.HideExceptions);
            Assert.AreEqual("Errors", config.TableName);
        }
    }
}
