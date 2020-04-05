using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Celerik.NetCore.Util.Test
{
    [TestClass]
    public class CryptoUtilityTest : UtilBaseTest
    {
        [TestMethod]
        public void HashPassword()
        {
            var password = "AlexaPlayLaPituca";
            var hash1 = CryptoUtility.HashPassword(password);
            var hash2 = CryptoUtility.HashPassword(password);

            Assert.AreEqual(true, hash1.Length > 0);
            Assert.AreEqual(true, hash2.Length > 0);
            Assert.AreNotEqual(hash1, hash2);

            TestUtility.AssertThrows<ArgumentNullException>(() =>
            {
                CryptoUtility.HashPassword(null);
            });
        }

        [TestMethod]
        public void VerifyHashedPassword()
        {
            var password = "AlexaPlayLaPequenaWendyZulca";
            var hash = CryptoUtility.HashPassword(password);
            var isValidHash = CryptoUtility.VerifyHashedPassword(hash, password);

            Assert.AreEqual(true, isValidHash);

            TestUtility.AssertThrows<ArgumentNullException>(() =>
            {
                CryptoUtility.VerifyHashedPassword(null, null);
            });
        }
    }
}
