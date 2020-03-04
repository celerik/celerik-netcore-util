using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Celerik.NetCore.Util.Test
{
    [TestClass]
    public class ValidateUtilityTest : UtilBaseTest
    {
        [TestMethod]
        public void IsValidBoolValidLowerTrue()
        {
            var boolean = "true";
            var isValid = boolean.IsValidBool();

            Assert.AreEqual(true, isValid);
        }

        [TestMethod]
        public void IsValidBoolValidUpperTrue()
        {
            var boolean = "True";
            var isValid = boolean.IsValidBool();

            Assert.AreEqual(true, isValid);
        }

        [TestMethod]
        public void IsValidBoolValidLowerFalse()
        {
            var boolean = "false";
            var isValid = boolean.IsValidBool();

            Assert.AreEqual(true, isValid);
        }

        [TestMethod]
        public void IsValidBoolValidUpperFalse()
        {
            var boolean = "False";
            var isValid = boolean.IsValidBool();

            Assert.AreEqual(true, isValid);
        }

        [TestMethod]
        public void IsValidBoolInvalid()
        {
            var boolean = "El jugo de remolacha es bueno";
            var isValid = boolean.IsValidBool();

            Assert.AreEqual(false, isValid);
        }

        [DataTestMethod]
        [DataRow("665")]
        [DataRow("665.99")]
        [DataRow("-665.99")]
        public void IsValidDecimalValid(string number)
        {
            Assert.AreEqual(true, number.IsValidDecimal());
        }

        [DataTestMethod]
        [DataRow("665,99")]
        [DataRow("$665")]
        [DataRow("22 acacia avenue")]
        public void IsValidDecimalInvalid(string number)
        {
            Assert.AreEqual(false, number.IsValidDecimal());
        }

        [DataTestMethod]
        [DataRow("665")]
        [DataRow("665.99")]
        [DataRow("-665.99")]
        public void IsValidDoubleValid(string number)
        {
            Assert.AreEqual(true, number.IsValidDouble());
        }

        [DataTestMethod]
        [DataRow("665,99")]
        [DataRow("$665")]
        [DataRow("22 acacia avenue")]
        public void IsValidDoubleInvalid(string number)
        {
            Assert.AreEqual(false, number.IsValidDouble());
        }

        [DataTestMethod]
        [DataRow("665")]
        [DataRow("665.99")]
        [DataRow("-665.99")]
        public void IsValidFloatValid(string number)
        {
            Assert.AreEqual(true, number.IsValidFloat());
        }

        [DataTestMethod]
        [DataRow("665,99")]
        [DataRow("$665")]
        [DataRow("22 acacia avenue")]
        public void IsValidFloatInvalid(string number)
        {
            Assert.AreEqual(false, number.IsValidFloat());
        }

        [DataTestMethod]
        [DataRow("665")]
        [DataRow("-665")]
        public void IsValidIntValid(string number)
        {
            Assert.AreEqual(true, number.IsValidInt());
        }

        [DataTestMethod]
        [DataRow("665,99")]
        [DataRow("$665")]
        [DataRow("22 acacia avenue")]
        public void IsValidIntInvalid(string number)
        {
            Assert.AreEqual(false, number.IsValidInt());
        }

        [TestMethod]
        public void IsValidUrlValid()
        {
            var url = "http://www.themostamazingwebsiteontheinternet.com/";
            var isValid = url.IsValidUrl();

            Assert.AreEqual(true, isValid);
        }

        [TestMethod]
        public void IsValidUrlInvalid()
        {
            var url = "http:// gene simmons is ugly . com";
            var isValid = url.IsValidUrl();

            Assert.AreEqual(false, isValid);
        }
    }
}
