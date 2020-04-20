using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Celerik.NetCore.Util.Test
{
    [TestClass]
    public class StackTraceUtilityTest : UtilBaseTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetMethodNameInvalid()
        {
            StackTrace stackTrace = null;
            stackTrace.GetMethodName();
        }

        [TestMethod]
        public void GetMethodNameValid()
        {
            var stackTrace = new StackTrace();
            var topMethodName = stackTrace.GetMethodName();

            Assert.AreEqual("StackTraceUtilityTest.GetMethodNameValid()", topMethodName);
        }
    }
}
