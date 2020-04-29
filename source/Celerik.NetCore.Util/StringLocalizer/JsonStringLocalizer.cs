using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Localization;

namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Represents a service that provides localized strings using
    /// JSON Files. Name of resource files should follow these
    /// conventions:
    ///     - "TypeName-language2-country2-region2.json"
    ///     - "TypeName-language2-country2.json"
    ///     - "TypeName-language2.json"
    /// </summary>
    public class JsonStringLocalizer : IStringLocalizer
    {
        /// <summary>
        /// The assembly where we get the resource files.
        /// </summary>
        private readonly Assembly _assembly;

        /// <summary>
        /// The base file name for resource files, it includes the
        /// ResourcesPath and the TypeName. E.g.: "Resources.UtilResources".
        /// </summary>
        private readonly string _baseFileName;

        /// <summary>
        /// Culture for which the resource strings will be loaded.
        /// </summary>
        private readonly CultureInfo _culture;

        /// <summary>
        /// List of localized strings for the current culture and its
        /// parents.
        /// </summary>
        private readonly IEnumerable<LocalizedString> _allStrings;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="assembly">The assembly where we get the resource
        /// files.</param>
        /// <param name="baseFileName">The base file name for resource files,
        /// it includes the ResourcesPath and the TypeName. E.g.:
        /// "Resources.UtilResources".</param>
        /// <param name="culture">Culture for which the resource strings
        /// will be loaded.</param>
        public JsonStringLocalizer(
            Assembly assembly, string baseFileName, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(baseFileName))
                throw new ArgumentException(nameof(baseFileName));

            _assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));
            _baseFileName = baseFileName;
            _culture = culture ?? throw new ArgumentNullException(nameof(culture));
            _allStrings = GetAllStrings(includeParentCultures: true);
        }

        /// <summary>
        /// Initializes a new instance of the class, using
        /// CultureInfo.CurrentCulture as the default culture to resolve
        /// resource strings.
        /// </summary>
        /// <param name="assembly">The assembly where we get the resource
        /// files.</param>
        /// <param name="baseFileName">The base file name for resource files,
        /// it includes the ResourcesPath and the TypeName. E.g.:
        /// "Resources.UtilResources".</param>
        public JsonStringLocalizer(Assembly assembly, string baseFileName)
            : this(assembly, baseFileName, CultureInfo.CurrentCulture) { }

        /// <summary>
        /// Gets the string resource with the given name.
        /// </summary>
        /// <param name="name">The name of the string resource.</param>
        /// <returns>String resource with the given name.</returns>
        public LocalizedString this[string name]
        {
            get
            {
                var item = _allStrings.FirstOrDefault(s => s.Name == name);

                if (item == null)
                {
                    var searchedLocation = GetSearchedLocation(_culture);

                    item = new LocalizedString
                    (
                        name,
                        value: null,
                        resourceNotFound: true,
                        searchedLocation
                    );
                }

                return item;
            }
        }

        /// <summary>
        /// Gets the string resource with the given name and
        /// formatted with the supplied arguments.
        /// </summary>
        /// <param name="name">The name of the string resource.</param>
        /// <param name="arguments">The name of the string resource.
        /// </param>
        /// <returns>String resource with the given name and formatted
        /// with the supplied arguments.</returns>
        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var item = this[name];

                if (!item.ResourceNotFound)
                    item = new LocalizedString
                    (
                        item.Name,
                        value: string.Format(_culture, item.Value, arguments),
                        resourceNotFound: false,
                        searchedLocation: item.SearchedLocation
                    );

                return item;
            }
        }

        /// <summary>
        /// Gets a list of localized strings for the current culture.
        /// </summary>
        /// <param name="includeParentCultures">A boolean indicating
        /// whether to include strings from parent cultures.</param>
        /// <returns>List of localized strings for the current culture.
        /// </returns>
        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            if (includeParentCultures && _allStrings != null)
                return _allStrings;

            var strings = GetStrings(_culture);

            if (includeParentCultures && !string.IsNullOrEmpty(_culture.Parent?.Name))
                strings.AddRange(WithCulture(_culture.Parent).GetAllStrings(true));

            return strings;
        }

        /// <summary>
        /// Creates a new JsonStringLocalizer for the passed-in culture.
        /// </summary>
        /// <param name="culture">Culture for which the resource strings
        /// will be loaded.</param>
        /// <returns>JsonStringLocalizer for the passed-in culture.
        /// </returns>
        public IStringLocalizer WithCulture(CultureInfo culture)
            => new JsonStringLocalizer(_assembly, _baseFileName, culture);

        /// <summary>
        /// Gets the list of resource strings corresponding to the
        /// passed-in culture.
        /// </summary>
        /// <param name="culture">Culture for which the resource strings
        /// will be loaded.</param>
        /// <returns>List of resource strings corresponding to the
        /// passed-in culture.</returns>
        private List<LocalizedString> GetStrings(CultureInfo culture)
        {
            var strings = new List<LocalizedString>();
            var fileName = GetFileName(culture);
            var namespce = _assembly.FullName.Split(',')[0];
            var searchedLocation = $"{namespce}.{fileName}";
            var resourceNames = _assembly.GetManifestResourceNames();

            if (resourceNames.Contains(searchedLocation))
            {
                var dictionary = EmbeddedFileUtility
                    .ReadJson<Dictionary<string, string>>(fileName, _assembly);

                foreach (var entry in dictionary)
                    strings.Add(new LocalizedString
                    (
                        name: entry.Key,
                        value: entry.Value,
                        resourceNotFound: false,
                        searchedLocation: searchedLocation
                    ));
            }

            return strings;
        }

        /// <summary>
        /// Gets the searched location of the passed-in culture.
        /// </summary>
        /// <param name="culture">Culture for which we are
        /// retrieving the searching location.</param>
        /// <returns>Searched location of the passed-in culture.</returns>
        private string GetSearchedLocation(CultureInfo culture)
        {
            var fileName = GetFileName(culture);
            var namespce = _assembly.FullName.Split(',')[0];
            var searchedLocation = $"{namespce}.{fileName}";

            return searchedLocation;
        }

        /// <summary>
        /// Gets the resource filename corresponding to the passed-in
        /// culture.
        /// </summary>
        /// <param name="culture">Culture for which we are retrieving
        /// the resource file name.</param>
        /// <returns>Resource filename corresponding to the passed-in
        /// culture.</returns>
        private string GetFileName(CultureInfo culture)
        {
            var normalizedName = culture.Name.Replace("/", "-");
            var fileName = $"{_baseFileName}-{normalizedName}.json";

            return fileName;
        }
    }
}
