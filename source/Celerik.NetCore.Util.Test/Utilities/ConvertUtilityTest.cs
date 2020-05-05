using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Celerik.NetCore.Util.Test
{
    [TestClass]
    public class ConvertUtilityTest
    {
        [DataTestMethod]
        [DataRow("true")]
        [DataRow("True")]
        public void ToBoolValid(string str)
            => Assert.AreEqual(true, str.ToBool());

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("Verdadero")]
        [DataRow("Falso")]
        [DataRow("TrUe")]
        [DataRow("FALSE")]
        public void ToBoolInvalid(string str)
            => Assert.AreEqual(default, str.ToBool());

        [TestMethod]
        [DataRow("1000000")]
        public void ToIntInvariantValid(string str)
            => Assert.AreEqual(1000000, str.ToIntInvariant());

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("1,1")]
        [DataRow("2147483648")]
        public void ToIntInvariantInvalid(string str)
            => Assert.AreEqual(default, str.ToIntInvariant());

        [TestMethod]
        public void ToDecimalValid()
        {
            var str = "1.1";
            var number = str.ToDecimalInvariant();
            Assert.AreEqual(1.1m, number);
        }

        [DataTestMethod]
        [DataRow("1,1")]
        [DataRow(null)]
        public void ToDecimalInvalid(string str)
        {
            var number = str.ToDecimalInvariant();
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
