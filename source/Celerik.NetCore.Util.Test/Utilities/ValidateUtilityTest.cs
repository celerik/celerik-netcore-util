using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Celerik.NetCore.Util.Test
{
    [TestClass]
    public class ValidateUtilityTest
    {
        [DataTestMethod]
        [DataRow("juan@crepsandwafles.com")]
        [DataRow("juan-123@postreselastor.com")]
        [DataRow("j@j.co")]
        public void IsValidEmailValid(string email)
            => Assert.AreEqual(true, email.IsValidEmail());

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("juan@pollosmario")]
        [DataRow("j@j.co.")]
        [DataRow("@postreselastor.com")]
        [DataRow("12345")]
        [DataRow("abcde.com")]
        [DataRow("https://abcde.com")]
        public void IsValidEmailInvalid(string email)
            => Assert.AreEqual(false, email.IsValidEmail());

        [DataTestMethod]
        [DataRow("Juan")]
        [DataRow("Juan David")]
        [DataRow("Juan David León")]
        [DataRow("A")]
        [DataRow("Ab")]
        public void IsValidNameValid(string name)
            => Assert.AreEqual(true, name.IsValidPersonName());

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("12345")]
        [DataRow("Juan David 19")]
        [DataRow("Juan ")]
        [DataRow(" Juan")]
        [TestMethod]
        public void IsValidNameInvalid(string name)
            => Assert.AreEqual(false, name.IsValidPersonName());








        [DataTestMethod]
        [DataRow("Juan123456**")]
        [DataRow("111AAAbbb###")]
        public void IsValidStandardPasswordValid(string password)
            => Assert.AreEqual(true, password.IsValidStandardPassword());

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("123")]
        [DataRow("123abc")]
        [DataRow("123abcDEF")]
        [DataRow("Ju1*")]
        public void IsValidStandardPasswordInvalid(string password)
            => Assert.AreEqual(false, password.IsValidStandardPassword());

        [DataTestMethod]
        [DataRow("12345")]
        [DataRow("12345-6789")]
        public void IsValidZipPlus4Valid(string zip)
            => Assert.AreEqual(true, zip.IsValidZipPlus4());

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("1234")]
        [DataRow("1234-5678")]
        public void IsValidZipPlus4Invalid(string zip)
            => Assert.AreEqual(false, zip.IsValidZipPlus4());







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
        public void IsValidDecimalInvariantValid(string number)
        {
            Assert.AreEqual(true, number.IsValidDecimalInvariant());
        }

        [DataTestMethod]
        [DataRow("665,99")]
        [DataRow("$665")]
        [DataRow("22 acacia avenue")]
        public void IsValidDecimalInvariantInvalid(string number)
        {
            Assert.AreEqual(false, number.IsValidDecimalInvariant());
        }

        [DataTestMethod]
        [DataRow("665")]
        [DataRow("665.99")]
        [DataRow("-665.99")]
        public void IsValidDoubleInvariantValid(string number)
        {
            Assert.AreEqual(true, number.IsValidDoubleInvariant());
        }

        [DataTestMethod]
        [DataRow("665,99")]
        [DataRow("$665")]
        [DataRow("22 acacia avenue")]
        public void IsValidDoubleInvariantInvalid(string number)
        {
            Assert.AreEqual(false, number.IsValidDoubleInvariant());
        }

        [DataTestMethod]
        [DataRow("665")]
        [DataRow("665.99")]
        [DataRow("-665.99")]
        public void IsValidFloatInvariantValid(string number)
        {
            Assert.AreEqual(true, number.IsValidFloatInvariant());
        }

        [DataTestMethod]
        [DataRow("665,99")]
        [DataRow("$665")]
        [DataRow("22 acacia avenue")]
        public void IsValidFloatInvariantInvalid(string number)
        {
            Assert.AreEqual(false, number.IsValidFloatInvariant());
        }

        [DataTestMethod]
        [DataRow("665")]
        [DataRow("-665")]
        public void IsValidIntInvariantValid(string number)
        {
            Assert.AreEqual(true, number.IsValidIntInvariant());
        }

        [DataTestMethod]
        [DataRow("665,99")]
        [DataRow("$665")]
        [DataRow("22 acacia avenue")]
        public void IsValidIntInvariantInvalid(string number)
        {
            Assert.AreEqual(false, number.IsValidIntInvariant());
        }

        [TestMethod]
        public void IsValidUrlValidHttp()
        {
            var url = "http://www.themostamazingwebsiteontheinternet.com/";
            var isValid = url.IsValidUrl();

            Assert.AreEqual(true, isValid);
        }

        [TestMethod]
        public void IsValidUrlValidHttps()
        {
            var url = "https://www.themostamazingwebsiteontheinternet.com/";
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
        public void IsValidDateInvariantValid()
        {
            var date = "2000/01/01";
            var isValid = date.IsValidDateInvariant();

            Assert.AreEqual(true, isValid);
        }

        [TestMethod]
        public void IsValidDateInvariantInvalid()
        {
            var date = "2000/50/01";
            var isValid = date.IsValidDateInvariant();

            Assert.AreEqual(false, isValid);
        }

        [TestMethod]
        public void IsValidInternationalPhoneValid()
        {
            var phone = "+573043499162";
            var isValid = phone.IsValidInternationalPhone();

            Assert.AreEqual(true, isValid);
        }

        [TestMethod]
        public void IsValidInternationalPhoneInvalid()
        {
            var phone = "444 44 44 (Ext. 59)";
            var isValid = phone.IsValidInternationalPhone();

            Assert.AreEqual(false, isValid);
        }

        
    }
}
