using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Celerik.NetCore.Util.Test
{
    [TestClass]
    public class DummyIConfigurationTest : UtilBaseTest
    {
        [TestMethod]
        public void SetAndGet()
        {
            using var config = new DummyIConfiguration();
            config["Almuerzo"] = "Bandeja Paisa";

            Assert.AreEqual("Bandeja Paisa", config["Almuerzo"]);
        }

        [TestMethod]
        public void InvalidKey()
        {
            using var config = new DummyIConfiguration();
            Assert.AreEqual(null, config["Cena"]);
        }
    }
}
