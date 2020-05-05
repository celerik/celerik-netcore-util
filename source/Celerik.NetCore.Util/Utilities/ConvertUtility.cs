using System;
using System.Globalization;

namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Provides conversion utilities.
    /// </summary>
    public static class ConvertUtility
    {
        /// <summary>
        /// Converts this string to a bool.
        /// </summary>
        /// <param name="str">The string to be converted.</param>
        /// <param name="defaultVal">Value to return by default in case the
        /// string is an invalid bool.</param>
        /// <returns>String converted to bool.</returns>
        public static bool ToBool(this string str, bool defaultVal = default)
            => str.IsValidBool()
                ? bool.Parse(str)
                : defaultVal;

        /// <summary>
        /// Converts this string to an int by using invariant culture.
        /// </summary>
        /// <param name="str">The string to be converted.</param>
        /// <param name="defaultVal">Value to return by default in case the
        /// string is an invalid int.</param>
        /// <returns>String converted to int.</returns>
        public static int ToIntInvariant(this string str, int defaultVal = default)
            => str.IsValidIntInvariant()
                ? int.Parse(str, CultureInfo.InvariantCulture)
                : defaultVal;

        /// <summary>
        /// Converts this string to a float by using invariant culture.
        /// </summary>
        /// <param name="str">The string to be converted.</param>
        /// <param name="defaultVal">Value to return by default in case the
        /// string is an invalid float.</param>
        /// <returns>String converted to Float.</returns>
        public static float ToFloat(this string str, float defaultVal = default)
            => str.IsValidFloatInvariant()
                ? float.Parse(str, CultureInfo.InvariantCulture)
                : defaultVal;

        /// <summary>
        /// Converts this string to a Double by using invariant culture.
        /// </summary>
        /// <param name="str">The string to be converted.</param>
        /// <param name="defaultVal">Value to return by default in case the
        /// convertion cann't be performed.</param>
        /// <returns>String converted to Double.</returns>
        public static double ToDouble(this string str, double defaultVal = default) 
            => str.IsValidDoubleInvariant()
                ? double.Parse(str, CultureInfo.InvariantCulture)
                : defaultVal;

        /// <summary>
        /// Converts this string to a decimal by using invariant culture.
        /// </summary>
        /// <param name="str">The string to be converted.</param>
        /// <param name="defaultVal">Value to return by default in case the
        /// string is an invalid decimal.</param>
        /// <returns>String converted to decimal.</returns>
        public static decimal ToDecimalInvariant(this string str, decimal defaultVal = default) 
            => str.IsValidDecimalInvariant()
                ? decimal.Parse(str, CultureInfo.InvariantCulture)
                : defaultVal;







        /// <summary>
        /// Convert this string to an DateTime by using invariant culture.
        /// </summary>
        /// <param name="str">The string to be converted.</param>
        /// <param name="defaultVal">Value to return by default in case the
        /// convertion cann't be performed.</param>
        /// <returns>String converted to DateTime</returns>
        public static DateTime ToDateTime(this string str, DateTime defaultVal = default)
            => str.IsValidDateInvariant()
                ? DateTime.Parse(str, CultureInfo.InvariantCulture)
                : defaultVal;
    }
}
