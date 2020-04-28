using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Celerik.NetCore.Util.Test
{
    [TestClass]
    public class ConvertUtilityTest
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
            Assert.AreEqual(default, boolean);
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
            Assert.AreEqual(default, number);
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
            Assert.AreEqual(default, number);
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
            Assert.AreEqual(default, number);
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
            Assert.AreEqual(default, number);
        }

        [TestMethod]
        public void ToDateTimeValid()
        {
            var str = "2020/12/31 22:50:36";
            var date = str.ToDateTime();
            Assert.AreEqual(2020, date.Year);
            Assert.AreEqual(12, date.Month);
            Assert.AreEqual(31, date.Day);
            Assert.AreEqual(22, date.Hour);
            Assert.AreEqual(50, date.Minute);
            Assert.AreEqual(36, date.Second);
        }

        [DataTestMethod]
        [DataRow("InvalidDate")]
        [DataRow(null)]
        public void ToDateTimeInvalid(string str)
        {
            var date = str.ToDateTime();
            Assert.AreEqual(default, date);
        }
    }
}
