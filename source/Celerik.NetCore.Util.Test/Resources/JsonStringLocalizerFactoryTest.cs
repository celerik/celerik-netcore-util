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
            _ = localizer.GetAllStrings(true);
        }

        [TestMethod]
        public void CreateFromBaseName()
        {
            var factory = new JsonStringLocalizerFactory("Resources");
            _ = factory.Create("Celerik.NetCore.Util", "UtilResources");
        }
    }
}
