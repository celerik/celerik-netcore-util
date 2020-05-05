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
    public class SerializeAllContractResolverTest
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

            Assert.AreEqual(true, metalJson.ContainsInvariant("James Hetfield"));
            Assert.AreEqual(true, metalJson.ContainsInvariant("Kirk Hammett"));
            Assert.AreEqual(true, metalJson.ContainsInvariant("Cliff Burton"));
            Assert.AreEqual(false, metalJson.ContainsInvariant("Lars Ulrich"));
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

            Assert.AreEqual(true, ironJson.ContainsInvariant("Paul Di'Anno"));
            Assert.AreEqual(true, ironJson.ContainsInvariant("Dave Murray"));
            Assert.AreEqual(true, ironJson.ContainsInvariant("Steve Harvey"));
            Assert.AreEqual(true, ironJson.ContainsInvariant("El Pibe Valderrama"));
        }
    }
}
