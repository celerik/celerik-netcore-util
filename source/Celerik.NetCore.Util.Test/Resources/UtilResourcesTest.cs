using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Celerik.NetCore.Util.Test
{
    [TestClass]
    public class UtilResourcesTest : UtilBaseTest
    {
        [TestMethod]
        public void NullFactoryGet()
        {
            var factory = UtilResources.Factory;
            UtilResources.Initialize(null);

            var name = "Common.ArgumentCanNotBeNull";
            var resource = UtilResources.Get(name);

            UtilResources.Initialize(factory);
            Assert.AreEqual(name, resource);
        }

        [TestMethod]
        public void NullFactoryGetWithArgs()
        {
            var factory = UtilResources.Factory;
            UtilResources.Initialize(null);

            var name = "Common.ArgumentCanNotBeNull";
            string args;
            var resource = UtilResources.Get(name, nameof(args));

            UtilResources.Initialize(factory);
            Assert.AreEqual(name, resource);
        }

        [TestMethod]
        public void Get()
        {
            var name = "Common.ArgumentCanNotBeNull";
            var resource = UtilResources.Get(name);

            Assert.AreEqual("'{0}' can not be null", resource);
        }

        [TestMethod]
        public void GetWithArgs()
        {
            var name = "Common.ArgumentCanNotBeNull";
            string args;
            var resource = UtilResources.Get(name, nameof(args));

            Assert.AreEqual("'args' can not be null", resource);
        }
    }
}
