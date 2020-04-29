using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Celerik.NetCore.Util.Test
{
    [TestClass]
    public class JsonStringLocalizerFactoryTest
    {
        [TestMethod]
        public void CreateFromType()
        {
            var factory = new JsonStringLocalizerFactory("Resources");
            var localizer = factory.Create(typeof(UtilResources));
        }

        [TestMethod]
        public void CreateFromBaseName()
        {
            var factory = new JsonStringLocalizerFactory("Resources");
            var localizer = factory.Create("Celerik.NetCore.Util", "UtilResources");
        }
    }
}
