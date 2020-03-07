using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Celerik.NetCore.Util.Test
{
    public enum FeelingType
    {
        [System.ComponentModel.Description(":(")]
        Sad = 1,

        [System.ComponentModel.Description(":)")]
        Happy = 2
    }

    public class Employee
    {
        [JsonConverter(typeof(EnumDescriptionJsonConverter))]
        public FeelingType? Feeling { get; set; }
    }

    [TestClass]
    public class EnumDescriptionJsonConverterTest : UtilBaseTest
    {
        [TestMethod]
        public void SerializeSettedValue()
        {
            var me = new Employee { Feeling = FeelingType.Sad };
            var meSerialized = JsonConvert.SerializeObject(me);

            Assert.AreEqual(true, meSerialized.Contains(":(", StringComparison.InvariantCulture));
        }

        [TestMethod]
        public void SerializeUnsettedValue()
        {
            var me = new Employee();
            var meSerialized = JsonConvert.SerializeObject(me);

            Assert.AreEqual(true, meSerialized.Contains("null", StringComparison.InvariantCulture));
        }

        [TestMethod]
        public void Deserialize()
        {
            var json = "{ Feeling: 2 }";
            var deserialized = JsonConvert.DeserializeObject<Employee>(json);

            Assert.AreEqual(FeelingType.Happy, deserialized.Feeling);
        }

        [TestMethod]
        public void WriteJsonNullWritter()
        {
            var converter = new EnumDescriptionJsonConverter();

            TestUtility.AssertThrows<ArgumentException>(() => {
                converter.WriteJson(null, "{}", new JsonSerializer());
            });

        }

        [TestMethod]
        public void ReadJson()
        {
            var converter = new EnumDescriptionJsonConverter();
            Assert.AreEqual(null, converter.ReadJson(null, null, null, null));

        }

        [TestMethod]
        public void CanRead()
        {
            var converter = new EnumDescriptionJsonConverter();
            Assert.AreEqual(false, converter.CanRead);
        }

        [TestMethod]
        public void CanConvert()
        {
            var obj = "";
            var converter = new EnumDescriptionJsonConverter();

            Assert.AreEqual(false, converter.CanConvert(obj.GetType()));
        }
    }
}
