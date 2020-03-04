using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Celerik.NetCore.Util.Test
{
    [TestClass]
    public class ConvertUtilityTest : UtilBaseTest
    {
        [TestMethod]
        public void ToBoolValid()
        {
            var str = "True";
            var boolean = str.ToBool();
            Assert.AreEqual(true, boolean);
        }

        [DataTestMethod]
        [DataRow("Verdadero")]
        [DataRow(null)]
        public void ToBoolInvalid(string str)
        {
            var boolean = str.ToBool();
            Assert.AreEqual(false, boolean);
        }

        [TestMethod]
        public void ToDecimalValid()
        {
            var str = "1.1";
            var number = str.ToDecimal();
            Assert.AreEqual(1.1m, number);
        }

        [DataTestMethod]
        [DataRow("1,1")]
        [DataRow(null)]
        public void ToDecimalInvalid(string str)
        {
            var number = str.ToDecimal();
            Assert.AreEqual(0m, number);
        }

        [TestMethod]
        public void ToDoubleValid()
        {
            var str = "1.1";
            var number = str.ToDouble();
            Assert.AreEqual(1.1, number);
        }

        [DataTestMethod]
        [DataRow("1,1")]
        [DataRow(null)]
        public void ToDoubleInvalid(string str)
        {
            var number = str.ToDouble();
            Assert.AreEqual(0.0, number);
        }

        [TestMethod]
        public void ToFloatValid()
        {
            var str = "1.1";
            var number = str.ToFloat();
            Assert.AreEqual(1.1f, number);
        }

        [DataTestMethod]
        [DataRow("1,1")]
        [DataRow(null)]
        public void ToFloatInvalid(string str)
        {
            var number = str.ToFloat();
            Assert.AreEqual(0.0f, number);
        }

        [TestMethod]
        public void ToIntValid()
        {
            var str = "-1";
            var number = str.ToInt();
            Assert.AreEqual(-1, number);
        }

        [DataTestMethod]
        [DataRow("1,1")]
        [DataRow(null)]
        public void ToIntInvalid(string str)
        {
            var number = str.ToInt();
            Assert.AreEqual(0, number);
        }
    }
}
