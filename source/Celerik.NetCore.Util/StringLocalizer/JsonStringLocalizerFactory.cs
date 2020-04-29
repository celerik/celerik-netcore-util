using System;
using System.Reflection;
using Microsoft.Extensions.Localization;

namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Represents a factory that creates JsonStringLocalizer instances.
    /// </summary>
    public class JsonStringLocalizerFactory : IStringLocalizerFactory
    {
        /// <summary>
        /// The relative path under application root where resource files
        /// are located. E.g.: "Resources".
        /// </summary>
        private readonly string _resourcesPath;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="resourcesPath">The relative path under application
        /// root where resource files are located. E.g.: "Resources".</param>
        public JsonStringLocalizerFactory(string resourcesPath = "")
            => _resourcesPath = resourcesPath;

        /// <summary>
        /// Creates a JsonStringLocalizer using the Assembly and FullName
        /// of the specified Type.
        /// </summary>
        /// <param name="resourceSource">The Type of the source resource.
        /// </param>
        /// <returns>A JsonStringLocalizer built with the passed-in
        /// parameters.</returns>
        public IStringLocalizer Create(Type resourceSource)
        {
            if (resourceSource == null)
                throw new ArgumentNullException(nameof(resourceSource));

            var assembly = resourceSource.Assembly;
            var typeName = resourceSource.Name;
            var baseFileName = string.IsNullOrEmpty(_resourcesPath)
                ? typeName
                : $"{_resourcesPath}.{typeName}";

            return new JsonStringLocalizer(assembly, baseFileName);
        }

        /// <summary>
        /// Creates a JsonStringLocalizer using the passed-in baseName
        /// and location.
        /// </summary>
        /// <param name="baseName">The base name of the resource to load
        /// strings from. E.g.: "Celerik.NetCore.Util".</param>
        /// <param name="location">The location to load resources from.
        /// E.g.: "UtilResources".</param>
        /// <returns>A JsonStringLocalizer built with the passed-in
        /// parameters.</returns>
        public IStringLocalizer Create(string baseName, string location)
        {
            if (string.IsNullOrEmpty(baseName))
                throw new ArgumentException(nameof(baseName));
            if (string.IsNullOrEmpty(location))
                throw new ArgumentException(nameof(location));

            var assembly = Assembly.LoadFrom($"{baseName}.dll");
            var baseFileName = string.IsNullOrEmpty(_resourcesPath)
                ? location
                : $"{_resourcesPath}.{location}";

            return new JsonStringLocalizer(assembly, baseFileName);
        }
    }
}
