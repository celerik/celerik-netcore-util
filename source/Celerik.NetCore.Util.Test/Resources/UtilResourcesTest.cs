using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Extensions.Localization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Celerik.NetCore.Util.Test
{
    public class StringLocalizerFactory : IStringLocalizerFactory
    {
        public IStringLocalizer Create(Type resourceSource)
            => new StringLocalizer();
        public IStringLocalizer Create(string baseName, string location)
            => throw new NotImplementedException();
    }

    public class StringLocalizer : IStringLocalizer
    {
        public LocalizedString this[string name]
            => new LocalizedString(name, name);
        public LocalizedString this[string name, params object[] arguments]
            => new LocalizedString(name, string.Format(name, arguments));
        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
            => throw new NotImplementedException();
        public IStringLocalizer WithCulture(CultureInfo culture)
            => throw new NotImplementedException();
    }

    [TestClass]
    public class UtilResourcesTest
    {
        [TestMethod]
        public void NullFactoryGet()
        {
            var currentFactory = UtilResources.Factory;
            UtilResources.Initialize(null);

            var name = "The database was deleted!";
            var resource = UtilResources.Get(name);

            Assert.AreEqual(name, resource);
            UtilResources.Initialize(currentFactory);
        }

        [TestMethod]
        public void NullFactoryGetWithArgs()
        {
            var currentFactory = UtilResources.Factory;
            UtilResources.Initialize(null);

            var name = "The {0} database couldn´t be deleted!";
            var args = "ChuckNorrisFacts";
            var resource = UtilResources.Get(name, args);

            Assert.AreEqual("The ChuckNorrisFacts database couldn´t be deleted!", resource);
            UtilResources.Initialize(currentFactory);
        }

        [TestMethod]
        public void NonNullFactoryGet()
        {
            var currentFactory = UtilResources.Factory;
            UtilResources.Initialize(new StringLocalizerFactory());

            var name = "The database was deleted!";
            var resource = UtilResources.Get(name);

            Assert.AreEqual(name, resource);
            UtilResources.Initialize(currentFactory);
        }

        [TestMethod]
        public void NonNullFactoryGetWithArgs()
        {
            var currentFactory = UtilResources.Factory;
            UtilResources.Initialize(new StringLocalizerFactory());

            var name = "The {0} database couldn´t be deleted!";
            var args = "ChuckNorrisFacts";
            var resource = UtilResources.Get(name, args);

            Assert.AreEqual("The ChuckNorrisFacts database couldn´t be deleted!", resource);
            UtilResources.Initialize(currentFactory);
        }
    }
}
