using System;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Celerik.NetCore.Util.Test
{
    public class DeveloperSettigs
    {
        public string ScreenSize { get; set; }
        public string CoffeeBrand { get; set; }
        public string PreferredMusic { get; set; }
        public string Pet { get; set; }
    }

    [TestClass]
    public class ConfigUtilityTest : UtilBaseTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadFromNullConfiguration()
        {
            IConfiguration config = null;
            config.Read<DeveloperSettigs>("DeveloperSettigs");
        }

        [TestMethod]
        [ExpectedException(typeof(ConfigException))]
        public void ReadKeyNotFound()
        {
            var config = GetService<IConfiguration>();
            config.Read<DeveloperSettigs>("DeveloperSettigs");
        }

        [TestMethod]
        public void ReadOk()
        {
            var config = GetService<IConfiguration>();
            config["DeveloperSettigs:ScreenSize"] = "100 inches";
            config["DeveloperSettigs:CoffeeBrand"] = "Café La Bastilla";
            config["DeveloperSettigs:PreferredMusic"] = "Ultra super dark satanic bloody metal";
            config["DeveloperSettigs:Pet"] = "Kitten";

            var settings = config.Read<DeveloperSettigs>("DeveloperSettigs");

            Assert.AreEqual("100 inches", settings.ScreenSize);
            Assert.AreEqual("Café La Bastilla", settings.CoffeeBrand);
            Assert.AreEqual("Ultra super dark satanic bloody metal", settings.PreferredMusic);
            Assert.AreEqual("Kitten", settings.Pet);
        }
    }
}
