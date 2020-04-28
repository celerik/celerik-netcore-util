using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Celerik.NetCore.Util.Test
{
    [TestClass]
    public class UtilResourcesTest
    {
        [TestMethod]
        public void NullFactoryGet()
        {
            var factory = UtilResources.Factory;
            UtilResources.Initialize(null);

            var name = "The database was deleted!";
            var resource = UtilResources.Get(name);

            Assert.AreEqual(name, resource);
            UtilResources.Initialize(factory);
        }

        [TestMethod]
        public void NullFactoryGetWithArgs()
        {
            var factory = UtilResources.Factory;
            UtilResources.Initialize(null);

            var name = "The {0} database couldn´t be deleted!";
            var args = "ChuckNorrisFacts";
            var resource = UtilResources.Get(name, args);

            Assert.AreEqual("The ChuckNorrisFacts database couldn´t be deleted!", resource);
            UtilResources.Initialize(factory);
        }

        [TestMethod]
        public void Get()
        {
            var name = "The database was deleted!";
            var resource = UtilResources.Get(name);

            Assert.AreEqual(name, resource);
        }

        [TestMethod]
        public void GetWithArgs()
        {
            var name = "The {0} database couldn´t be deleted!";
            var args = "ChuckNorrisFacts";
            var resource = UtilResources.Get(name, args);

            Assert.AreEqual("The ChuckNorrisFacts database couldn´t be deleted!", resource);
        }
    }
}
