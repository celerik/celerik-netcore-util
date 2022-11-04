using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Celerik.NetCore.Util.Test
{
    [TestClass]
    public class JsonStringLocalizerFactoryTest
    {
        [TestMethod]
        [Obsolete]
        public void CreateFromType()
        {
            var factory = new JsonStringLocalizerFactory("Resources");
            var localizer = factory.Create(typeof(UtilResources));
            _ = localizer.WithCulture(new System.Globalization.CultureInfo("es")).GetAllStrings(true);
        }

        [TestMethod]
        public void CreateFromBaseName()
        {
            var factory = new JsonStringLocalizerFactory("Resources");
            _ = factory.Create("Celerik.NetCore.Util", "UtilResources");
        }
    }
}
