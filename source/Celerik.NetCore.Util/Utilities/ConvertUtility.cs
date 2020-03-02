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
        /// Converts this string to a Boolean.
        /// </summary>
        /// <param name="str">The string to be converted.</param>
        /// <param name="defaultVal">Value to return by default in case the
        /// convertion cann't be performed.</param>
        /// <returns>String converted to Boolean.</returns>
        public static bool ToBool(this string str, bool defaultVal = false) =>
            str.IsValidBool()
                ? bool.Parse(str)
                : defaultVal;

        /// <summary>
        /// Converts this string to a Decimal by using invariant culture.
        /// </summary>
        /// <param name="str">The string to be converted.</param>
        /// <param name="defaultVal">Value to return by default in case the
        /// convertion cann't be performed.</param>
        /// <returns>String converted to Decimal.</returns>
        public static decimal ToDecimal(this string str, decimal defaultVal = 0) =>
            str.IsValidDecimal()
                ? decimal.Parse(str, CultureInfo.InvariantCulture)
                : defaultVal;

        /// <summary>
        /// Converts this string to a Double by using invariant culture.
        /// </summary>
        /// <param name="str">The string to be converted.</param>
        /// <param name="defaultVal">Value to return by default in case the
        /// convertion cann't be performed.</param>
        /// <returns>String converted to Double.</returns>
        public static double ToDouble(this string str, double defaultVal = 0) =>
            str.IsValidDouble()
                ? double.Parse(str, CultureInfo.InvariantCulture)
                : defaultVal;

        /// <summary>
        /// Converts this string to a Float by using invariant culture.
        /// </summary>
        /// <param name="str">The string to be converted.</param>
        /// <param name="defaultVal">Value to return by default in case the
        /// convertion cann't be performed.</param>
        /// <returns>String converted to Float.</returns>
        public static float ToFloat(this string str, float defaultVal = 0) =>
            str.IsValidFloat()
                ? float.Parse(str, CultureInfo.InvariantCulture)
                : defaultVal;

        /// <summary>
        /// Converts this string to an Int by using invariant culture.
        /// </summary>
        /// <param name="str">The string to be converted.</param>
        /// <param name="defaultVal">Value to return by default in case the
        /// convertion cann't be performed.</param>
        /// <returns>String converted to Int.</returns>
        public static int ToInt(this string str, int defaultVal = 0) =>
            str.IsValidInt()
                ? int.Parse(str, CultureInfo.InvariantCulture)
                : defaultVal;

        /// <summary>
        /// Convert this string to an DateTime by using invariant culture.
        /// </summary>
        /// <param name="str">The string to be converted.</param>
        /// <returns>String converted to DateTime</returns>
        public static DateTime ToDateTime(this string str) =>
            str.IsValidDate()
                ? DateTime.Parse(str, CultureInfo.InvariantCulture)
                : DateTime.MinValue;
    }
}
