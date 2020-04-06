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
            var boolean = "El jugo de remolacha es nutritivo";
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
            var url = "http:// frisby . com";
            var isValid = url.IsValidUrl();

            Assert.AreEqual(false, isValid);
        }

        [TestMethod]
        public void IsValidDateValid()
        {
            var date = "2000/01/01";
            var isValid = date.IsValidDate();

            Assert.AreEqual(true, isValid);
        }

        [TestMethod]
        public void IsValidDateInvalid()
        {
            var date = "2000/50/01";
            var isValid = date.IsValidDate();

            Assert.AreEqual(false, isValid);
        }

        [TestMethod]
        public void IsValidEmailValid()
        {
            var email = "juan@pollosmario.com";
            var isValid = email.IsValidEmail();

            Assert.AreEqual(true, isValid);
        }

        [TestMethod]
        public void IsValidEmailInvalid()
        {
            var email = "juan@pollosmario";
            var isValid = email.IsValidEmail();

            Assert.AreEqual(false, isValid);
        }

        [TestMethod]
        public void IsValidNameValid()
        {
            var name = "José Covid";
            var isValid = name.IsValidName();

            Assert.AreEqual(true, isValid);
        }

        [TestMethod]
        public void IsValidNameInvalid()
        {
            var name = "José Covid 19";
            var isValid = name.IsValidName();

            Assert.AreEqual(false, isValid);
        }

        [TestMethod]
        public void IsValidPhoneNumberValid()
        {
            var phone = "444 44 44";
            var isValid = phone.IsValidPhoneNumber();

            Assert.AreEqual(true, isValid);
        }

        [TestMethod]
        public void IsValidPhoneNumberInvvalid()
        {
            var phone = "444 44 44 (Ext. 59)";
            var isValid = phone.IsValidPhoneNumber();

            Assert.AreEqual(false, isValid);
        }

        [TestMethod]
        public void IsValidZipValid()
        {
            var zip = "123456";
            var isValid = zip.IsValidZip();

            Assert.AreEqual(true, isValid);
        }

        [TestMethod]
        public void IsValidZipInvalid()
        {
            var zip = "123";
            var isValid = zip.IsValidZip();

            Assert.AreEqual(false, isValid);
        }
    }
}
