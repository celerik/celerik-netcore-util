using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Celerik.NetCore.Util.Test
{
    public class Metallica
    {
        public string JamesHetfield { get; set; }
        public string KirkHammett { get; set; }
        public string CliffBurton { get; set; }

        [JsonIgnore]
        public string LarsUlrich { get; set; }
    }

    [TestClass]
    public class SerializeAllContractResolverTest : UtilBaseTest
    {
        [TestMethod]
        public void WithoutContractResolver()
        {
            var metallica = new Metallica
            {
                JamesHetfield = "Cool guy",
                KirkHammett = "Cool guy",
                CliffBurton = "Cool guy",
                LarsUlrich = "Sucks",
            };

            var json = JsonConvert.SerializeObject(metallica);

            Assert.AreEqual(true, json.Contains("\"JamesHetfield\":", StringComparison.InvariantCulture));
            Assert.AreEqual(true, json.Contains("\"KirkHammett\":", StringComparison.InvariantCulture));
            Assert.AreEqual(true, json.Contains("\"CliffBurton\":", StringComparison.InvariantCulture));
            Assert.AreEqual(false, json.Contains("\"LarsUlrich\":", StringComparison.InvariantCulture));
        }

        [TestMethod]
        public void WithContractResolver()
        {
            var metallica = new Metallica
            {
                JamesHetfield = "Cool guy",
                KirkHammett = "Cool guy",
                CliffBurton = "Cool guy",
                LarsUlrich = "Sucks"
            };

            var settings = new JsonSerializerSettings
            {
                ContractResolver = new SerializeAllContractResolver()
            };

            var json = JsonConvert.SerializeObject(metallica, settings);

            Assert.AreEqual(true, json.Contains("\"JamesHetfield\":", StringComparison.InvariantCulture));
            Assert.AreEqual(true, json.Contains("\"KirkHammett\":", StringComparison.InvariantCulture));
            Assert.AreEqual(true, json.Contains("\"CliffBurton\":", StringComparison.InvariantCulture));
            Assert.AreEqual(true, json.Contains("\"LarsUlrich\":", StringComparison.InvariantCulture));
        }
    }
}
