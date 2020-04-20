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
            var expected = name;

            Assert.AreEqual(expected, resource);
            UtilResources.Initialize(factory);
        }

        [TestMethod]
        public void NullFactoryGetWithArgs()
        {
            var factory = UtilResources.Factory;
            UtilResources.Initialize(null);

            var name = "Common.ArgumentCanNotBeNull";
            string args;
            var resource = UtilResources.Get(name, nameof(args));
            var expected = name;

            Assert.AreEqual(expected, resource);
            UtilResources.Initialize(factory);
        }

        [TestMethod]
        public void Get()
        {
            var name = "Common.UnexistingResource";
            var resource = UtilResources.Get(name);
            var expected = name;

            Assert.AreEqual(expected, resource);
        }

        [TestMethod]
        public void GetWithArgs()
        {
            var name = "Common.UnexistingResource";
            string args;
            var resource = UtilResources.Get(name, nameof(args));
            var expected = name;

            Assert.AreEqual(expected, resource);
        }
    }
}
