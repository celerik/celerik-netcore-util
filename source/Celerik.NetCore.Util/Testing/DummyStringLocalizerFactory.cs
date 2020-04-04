using System;
using Microsoft.Extensions.Localization;

namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Implementation of the IStringLocalizerFactory interface for testing purposes. With
    /// this implementation we create dummy IStringLocalizer objects.
    /// </summary>
    public class DummyStringLocalizerFactory : IStringLocalizerFactory
    {
        /// <summary>
        /// Creates an Microsoft.Extensions.Localization.IStringLocalizer using the
        /// System.Reflection.Assembly and System.Type.FullName of the specified System.Type.
        /// </summary>
        /// <param name="resourceSource">The System.Type.</param>
        /// <returns>The Microsoft.Extensions.Localization.IStringLocalizer.</returns>
        public IStringLocalizer Create(Type resourceSource)
        {
            return new DummyStringLocalizer();
        }

        /// <summary>
        /// Creates an Microsoft.Extensions.Localization.IStringLocalizer.
        /// </summary>
        /// <param name="baseName">The base name of the resource to load strings from.</param>
        /// <param name="location">The location to load resources from.</param>
        /// <returns>The Microsoft.Extensions.Localization.IStringLocalizer.</returns>
        public IStringLocalizer Create(string baseName, string location)
        {
            return new DummyStringLocalizer();
        }
    }
}
