using System.Globalization;
using System.Linq;

namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Provides string utilities.
    /// </summary>
    public static class StringUtility
    {
        /// <summary>
        /// Converts the first char of this string to UpperCase and the rest to
        /// LowerCase.
        /// </summary>
        /// <param name="str">The string to be converted.</param>
        /// <returns>First char in UpperCase and rest in LowerCase.</returns>
        public static string FirstUpperRestLower(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            var firstUpper = str.First()
                .ToString(CultureInfo.InvariantCulture)
                .ToUpperInvariant();

            var restLower = str
                .Substring(1)
                .ToLowerInvariant();

            var newStr = $"{firstUpper}{restLower}";
            return newStr;
        }
    }
}
