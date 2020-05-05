using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Celerik.NetCore.Util.Test
{
    [TestClass]
    public class CryptoUtilityTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void HashPassword()
        {
            var password = "AlexaPlayLaPituca";
            var hash1 = CryptoUtility.HashPassword(password);
            var hash2 = CryptoUtility.HashPassword(password);

            Assert.AreEqual(true, hash1.Length > 0);
            Assert.AreEqual(true, hash2.Length > 0);
            Assert.AreNotEqual(hash1, hash2);

            CryptoUtility.HashPassword(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void VerifyHashedPassword()
        {
            var password = "AlexaPlayLaPequenaWendyZulca";
            var hash = CryptoUtility.HashPassword(password);
            var isValidHash = CryptoUtility.VerifyHashedPassword(hash, password);

            Assert.AreEqual(true, isValidHash);

            CryptoUtility.VerifyHashedPassword(null, null);
        }
    }
}
