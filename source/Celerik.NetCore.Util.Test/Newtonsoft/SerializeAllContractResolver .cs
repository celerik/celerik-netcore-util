using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Celerik.NetCore.Util.Test
{
    public class MetalBand
    {
        public string Singer { get; set; }
        public string LeadGuitar { get; set; }
        public string Bassist { get; set; }
        [JsonIgnore] public string Trumpeter { get; set; }
    }

    [TestClass]
    public class SerializeAllContractResolverTest : UtilBaseTest
    {
        [TestMethod]
        public void WithoutContractResolver()
        {
            var metallica = new MetalBand
            {
                Singer = "James Hetfield",
                LeadGuitar = "Kirk Hammett",
                Bassist = "Cliff Burton",
                Trumpeter = "Lars Ulrich",
            };

            var metalJson = JsonConvert.SerializeObject(metallica);

            Assert.AreEqual(true, metalJson.Contains("James Hetfield", StringComparison.InvariantCulture));
            Assert.AreEqual(true, metalJson.Contains("Kirk Hammett", StringComparison.InvariantCulture));
            Assert.AreEqual(true, metalJson.Contains("Cliff Burton", StringComparison.InvariantCulture));
            Assert.AreEqual(false, metalJson.Contains("Lars Ulrich", StringComparison.InvariantCulture));
        }

        [TestMethod]
        public void WithContractResolver()
        {
            var ironMaiden = new MetalBand
            {
                Singer = "Paul Di'Anno",
                LeadGuitar = "Dave Murray",
                Bassist = "Steve Harvey",
                Trumpeter = "El Pibe Valderrama"
            };

            var ironSettings = new JsonSerializerSettings
            {
                ContractResolver = new SerializeAllContractResolver()
            };

            var ironJson = JsonConvert.SerializeObject(ironMaiden, ironSettings);

            Assert.AreEqual(true, ironJson.Contains("Paul Di'Anno", StringComparison.InvariantCulture));
            Assert.AreEqual(true, ironJson.Contains("Dave Murray", StringComparison.InvariantCulture));
            Assert.AreEqual(true, ironJson.Contains("Steve Harvey", StringComparison.InvariantCulture));
            Assert.AreEqual(true, ironJson.Contains("El Pibe Valderrama", StringComparison.InvariantCulture));
        }
    }
}
