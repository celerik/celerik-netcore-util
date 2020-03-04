using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Celerik.NetCore.Util.Test
{
    [TestClass]
    public class StackTraceUtilityTest : UtilBaseTest
    {
        [TestMethod]
        public void GetMethodName()
        {
            var stackTrace = new StackTrace();
            var topMethodName = stackTrace.GetMethodName();

            Assert.AreEqual("StackTraceUtilityTest.GetMethodName()", topMethodName);
        }
    }
}
