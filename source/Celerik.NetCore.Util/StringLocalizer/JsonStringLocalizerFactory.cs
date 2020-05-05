using System;
using System.Reflection;
using Microsoft.Extensions.Localization;

namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Represents a factory that creates JsonStringLocalizer instances.
    /// 
    /// JsonStringLocalizer is a replacement of the .resx files
    /// system, using .json files instead.
    /// 
    /// When working with JSON resource files, take into account the
    /// following instructions:
    /// 
    /// 1. Create a folder in your project to hold all JSON resource
    ///    files. The recommended name for this folder is "Resources".
    /// 2. Create the TypeName class. This is a static class (can be
    ///    empty) placed in the resources folder. This class will be
    ///    used to resolve the assembly where resource files are
    ///    contained, and also to resolve the base file name of your
    ///    resource files. The Recomended name for this class is
    ///    "{SomePrefix}Resources.cs". E.g.: "UtilResources.cs".
    /// 3. Create your JSON resources files in your resources folder,
    ///    you should set them as embedded resources and name those
    ///    files following these conventions:
    ///         - "TypeName.json"
    ///         - "TypeName-language.json"
    ///         - "TypeName-language-country.json"
    ///         - "TypeName-language-country-region.json"
    ///  4. Resource strings are resolved from most specif to less
    ///     specific cultures, so if no file for a culture is found,
    ///     TypeName.json might be used to resolve your strings. The
    ///     recomendation is to put in this file all your localized
    ///     strings in "English" language, that way english will be
    ///     the default resource culture.
    ///  5. A good example of JSON resource files for a project
    ///     supporting English and Spanish could be as follow:
    ///         - UtilResources.cs
    ///         - UtilResources.json (default culture: english strings)
    ///         - UtilResources-es.json
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
        /// root where resource files are located. E.g.: "Resources". By
        /// default this is set to "Resources".</param>
        /// <exception cref="ArgumentNullException">If resourcesPath is null.
        /// </exception>
        public JsonStringLocalizerFactory(string resourcesPath = "Resources")
            => _resourcesPath = resourcesPath
                ?? throw new ArgumentNullException(nameof(resourcesPath));

        /// <summary>
        /// Creates a JsonStringLocalizer using the Assembly and Name
        /// of the specified Type.
        /// </summary>
        /// <param name="resourceSource">The Type of the source resource.
        /// </param>
        /// <returns>A JsonStringLocalizer built with the passed-in
        /// Type.</returns>
        /// <exception cref="ArgumentNullException">If resourceSource is null.
        /// </exception>
        public IStringLocalizer Create(Type resourceSource)
        {
            if (resourceSource == null)
                throw new ArgumentNullException(nameof(resourceSource));

            var assembly = resourceSource.Assembly;
            var typeName = resourceSource.Name;
            var baseFileName = _resourcesPath.Length == 0
                ? typeName
                : $"{_resourcesPath}.{typeName}";

            return new JsonStringLocalizer(assembly, baseFileName);
        }

        /// <summary>
        /// Creates a JsonStringLocalizer using the passed-in baseName
        /// and location.
        /// </summary>
        /// <param name="baseName">The name of the assembly where
        /// JSON resource files are embedded. E.g.:
        /// "Celerik.NetCore.Util".</param>
        /// <param name="location">The base name of your resource
        /// files. E.g.: "UtilResources".</param>
        /// <returns>A JsonStringLocalizer built with the passed-in
        /// baseName and location.</returns>
        /// <exception cref="ArgumentNullException">If baseName or
        /// location are null or empty.</exception>
        /// <exception cref="DllNotFoundException">If a .DLL with
        /// the provided baseName was not found.</exception>
        public IStringLocalizer Create(string baseName, string location)
        {
            if (string.IsNullOrEmpty(baseName))
                throw new ArgumentNullException(nameof(baseName));
            if (string.IsNullOrEmpty(location))
                throw new ArgumentNullException(nameof(location));

            var assemblyName = $"{baseName}.dll";
            if (!FileUtility.ExistsFileInExecutingAssemblyDir(assemblyName))
                throw new DllNotFoundException(
                    UtilResources.Get("DllNotFound", assemblyName));

            var assembly = Assembly.LoadFrom(assemblyName);
            var baseFileName = _resourcesPath.Length == 0
                ? location
                : $"{_resourcesPath}.{location}";

            return new JsonStringLocalizer(assembly, baseFileName);
        }
    }
}
