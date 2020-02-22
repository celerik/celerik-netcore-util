using System.Collections.Generic;
using System.Globalization;
using Microsoft.Extensions.Localization;

namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Implementation of the IStringLocalizer interface for testing purposes. With
    /// this implementation we localize string values in memory.
    /// </summary>
    public class DummyIStringLocalizer : IStringLocalizer
    {
        /// <summary>
        /// Gets the string resource with the given name.
        /// </summary>
        /// <param name="name">The name of the string resource.</param>
        /// <returns>The string resource as a LocalizedString.</returns>
        public LocalizedString this[string name] =>
            new LocalizedString(name, name);

        /// <summary>
        /// Gets the string resource with the given name and formatted with
        /// the supplied arguments.
        /// </summary>
        /// <param name="name">The name of the string resource.</param>
        /// <param name="arguments">The values to format the string with.</param>
        /// <returns>The formatted string resource as a LocalizedString.</returns>
        public LocalizedString this[string name, params object[] arguments] =>
             new LocalizedString(name, string.Format(name, arguments));

        /// <summary>
        /// Gets all string resources.
        /// </summary>
        /// <param name="includeParentCultures">A System.Boolean indicating whether
        /// to include strings from parent cultures.</param>
        /// <returns>The strings.</returns>
        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) =>
            new List<LocalizedString>();

        /// <summary>
        /// Creates a new Microsoft.Extensions.Localization.IStringLocalizer for
        /// a specific System.Globalization.CultureInfo.
        /// </summary>
        /// <param name="culture">The System.Globalization.CultureInfo to use.</param>
        /// <returns>A culture-specific IStringLocalizer.</returns>
        public IStringLocalizer WithCulture(CultureInfo culture) =>
            new DummyIStringLocalizer();
    }
}
