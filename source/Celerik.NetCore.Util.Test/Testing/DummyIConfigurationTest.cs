using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Celerik.NetCore.Util.Test
{
    [TestClass]
    public class DummyConfigurationTest : UtilBaseTest
    {
        [TestMethod]
        public void SetAndGet()
        {
            using var config = new DummyConfiguration();
            config["Almuerzo"] = "Bandeja Paisa";

            Assert.AreEqual("Bandeja Paisa", config["Almuerzo"]);
        }

        [TestMethod]
        public void InvalidKey()
        {
            using var config = new DummyConfiguration();
            Assert.AreEqual(null, config["Cena"]);
        }
    }
}
