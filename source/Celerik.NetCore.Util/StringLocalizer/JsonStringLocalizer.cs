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
    /// .json Files in replace of .resx files.
    /// </summary>
    public class JsonStringLocalizer : IStringLocalizer
    {
        /// <summary>
        /// The assembly where we get the resource files.
        /// </summary>
        private readonly Assembly _assembly;

        /// <summary>
        /// The base file name of the resource files. It includes the
        /// ResourcesPath and the TypeName. E.g.: "Resources.UtilResources".
        /// </summary>
        private readonly string _baseFileName;

        /// <summary>
        /// Culture for which the resource strings will be loaded.
        /// </summary>
        private readonly CultureInfo _culture;

        /// <summary>
        /// List with all localized strings for the defined culture. Strings
        /// from parent cultures are contained here as well. Strings are
        /// ordered from more specific culture strings at the top to less
        /// specific culture strings at the bottom.
        /// </summary>
        private readonly IEnumerable<LocalizedString> _allStrings;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="assembly">The assembly where we get the resource files.
        /// </param>
        /// <param name="baseFileName">The base file name of the resource files.
        /// It includes the ResourcesPath and the TypeName. E.g.:
        /// "Resources.UtilResources".</param>
        /// <param name="culture">Culture for which the resource strings
        /// will be loaded.</param>
        /// <exception cref="ArgumentNullException">If assembly is null, or
        /// baseFileName is null or empty, or culture is null.</exception>
        public JsonStringLocalizer(
            Assembly assembly, string baseFileName, CultureInfo culture)
        {
            if (baseFileName != null && baseFileName.Length == 0)
                throw new ArgumentException(
                    UtilResources.Get("ArgumentCanNotBeEmpty", nameof(baseFileName)));

            _assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));
            _baseFileName = baseFileName ?? throw new ArgumentNullException(nameof(baseFileName)); ;
            _culture = culture ?? throw new ArgumentNullException(nameof(culture));
            _allStrings = GetAllStrings(includeParentCultures: true);
        }

        /// <summary>
        /// Initializes a new instance of the class, using
        /// CultureInfo.CurrentCulture as the defined culture to resolve
        /// resource strings.
        /// </summary>
        /// <param name="assembly">The assembly where we get the resource
        /// files.</param>
        /// <param name="baseFileName">The base file name of the resource files.
        /// It includes the ResourcesPath and the TypeName. E.g.:
        /// "Resources.UtilResources".</param>
        /// <exception cref="ArgumentNullException">If assembly is null, or
        /// baseFileName is null or empty.</exception>
        public JsonStringLocalizer(Assembly assembly, string baseFileName)
            : this(assembly, baseFileName, CultureInfo.CurrentCulture) { }

        /// <summary>
        /// Gets the file name of resource strings for the defined
        /// culture. E.g.: "Resources.UtilResources-es.json".
        /// </summary>
        /// <returns>File name of resource strings for the defined
        /// culture.</returns>
        private string CultureFileName
        {
            get
            {
                var cultureName = _culture.Name;
                var dashedName = cultureName.Length == 0
                    ? ""
                    : $"-{cultureName}";
                var fileName = $"{_baseFileName}{dashedName}.json";

                return fileName;
            }
        }

        /// <summary>
        /// Gets the search location of resource strings for the defined
        /// culture. E.g.: "Celerik.NetCore.Util.Resources.UtilResources-es.json".
        /// </summary>
        /// <returns>Search location of resource strings for the defined
        /// culture.</returns>
        private string CultureSearchLocation
        {
            get
            {
                var assemblyNamespace = _assembly.FullName.Split(',')[0];
                var searchLocation = $"{assemblyNamespace}.{CultureFileName}";

                return searchLocation;
            }
        }

        /// <summary>
        /// Gets the list of resource strings for the defined culture,
        /// ordered by resource name ascending. Strings from parent cultures
        /// are not taken into account.
        /// </summary>
        /// <returns>List of resource strings for the defined culture.
        /// </returns>
        private List<LocalizedString> CultureStrings
        {
            get
            {
                var strings = new List<LocalizedString>();
                var resourceNames = _assembly.GetManifestResourceNames();

                if (resourceNames.Contains(CultureSearchLocation))
                {
                    var dictionary = EmbeddedFileUtility
                        .ReadJson<Dictionary<string, string>>(CultureFileName, _assembly)
                        .OrderBy(entry => entry.Key);

                    foreach (var entry in dictionary)
                        strings.Add(new LocalizedString
                        (
                            name: entry.Key,
                            value: entry.Value,
                            resourceNotFound: false,
                            searchedLocation: CultureSearchLocation
                        ));
                }

                return strings;
            }
        }

        /// <summary>
        /// Gets the string resource with the given name.
        /// </summary>
        /// <param name="name">The name of the string resource.</param>
        /// <returns>String resource with the given name.</returns>
        public LocalizedString this[string name]
        {
            get
            {
                var item = _allStrings.FirstOrDefault(str => str.Name == name)
                    ?? new LocalizedString(
                        name,
                        value: name,
                        resourceNotFound: true,
                        searchedLocation: CultureSearchLocation
                    );

                return item;
            }
        }

        /// <summary>
        /// Gets the string resource with the given name and formatted
        /// with the supplied arguments.
        /// </summary>
        /// <param name="name">The name of the string resource.</param>
        /// <param name="arguments">The values to format the string with.</param>
        /// <returns>String resource with the given name and formatted
        /// with the supplied arguments.</returns>
        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var item = this[name];

                var formatedItem = new LocalizedString(
                    name,
                    string.Format(_culture, item.Value, arguments),
                    resourceNotFound: item.ResourceNotFound,
                    searchedLocation: item.SearchedLocation
                );

                return formatedItem;
            }
        }

        /// <summary>
        /// Gets a list with all localized strings for the defined
        /// culture. Strings from parent cultures can be optionally
        /// added. Returned strings are ordered from more specific
        /// culture strings at the top to less specific culture strings
        /// at the bottom.
        /// </summary>
        /// <param name="includeParentCultures">A boolean indicating
        /// whether to include strings from parent cultures.</param>
        /// <returns>List with all localized strings for the defined
        /// culture.</returns>
        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            if (includeParentCultures && _allStrings != null)
                return _allStrings;

            var allStrings = CultureStrings;

            includeParentCultures &= _culture.Parent != null;
            includeParentCultures &= _culture.Parent.Name != _culture.Name;

            if (includeParentCultures)
            {
                var parentLocalizer = WithCulture(_culture.Parent);
                var parentAllStrings = parentLocalizer.GetAllStrings(true);

                allStrings.AddRange(parentAllStrings);
            }

            return allStrings;
        }

        /// <summary>
        /// Creates a new JsonStringLocalizer with the same assembly and
        /// baseFileName of this class, and the passed-in culture.
        /// </summary>
        /// <param name="culture">Culture for which the resource strings
        /// will be loaded.</param>
        /// <returns>JsonStringLocalizer with the same assembly and
        /// baseFileName of this class, and the passed-in culture.</returns>
        public IStringLocalizer WithCulture(CultureInfo culture)
            => new JsonStringLocalizer(_assembly, _baseFileName, culture);
    }
}
