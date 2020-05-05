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
#pragma warning disable CS0618 // Type or member is obsolete
            var all = localizer.WithCulture(new System.Globalization.CultureInfo("es")).GetAllStrings(true);
#pragma warning restore CS0618 // Type or member is obsolete
        }

        [TestMethod]
        public void CreateFromBaseName()
        {
            var factory = new JsonStringLocalizerFactory("Resources");
            var localizer = factory.Create("Celerik.NetCore.Util", "UtilResources");
        }
    }
}
