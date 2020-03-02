using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Provides validation utilities.
    /// </summary>
    public static class ValidateUtility
    {
        /// <summary>
        /// Regex for email.
        /// </summary>
        public const string EmailRegex = @"^(([^<>()\[\]\\.,;:\s@""]+(\.[^<>()\[\]\\.,;:\s@""]+)*)|("".+""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$";

        /// <summary>
        /// Regex for names (only allows letters and spaces).
        /// </summary>
        public const string NameRegex = @"^[A-zÀ-ú ]+$";

        /// <summary>
        /// Regex for phone numbers.
        /// </summary>
        public const string PhoneRegex = @"^(?:\((\+?\d+)?\)|\+?\d+) ?\d*(-?\d{2,3} ?){0,4}$";

        /// <summary>
        /// Regex for Zip codes.
        /// </summary>
        public const string ZipRegex = @"^[0-9]{5,6}(?:-[0-9]{4})?$";

        /// <summary>
        /// Validates whether this string is a valid boolean.
        /// </summary>
        /// <param name="str">The string to validate.</param>
        /// <returns>True if this string is a valid boolean.</returns>
        public static bool IsValidBool(this string str) =>
            str?.ToLowerInvariant() == "true" || str?.ToLowerInvariant() == "false";

        /// <summary>
        /// Validates whether this string is a valid Decimal with invariant culture.
        /// </summary>
        /// <param name="str">The string to validate.</param>
        /// <returns>True if this string is a valid Decimal.</returns>
        public static bool IsValidDecimal(this string str) =>
            decimal.TryParse(
                str,
                NumberStyles.AllowDecimalPoint |
                NumberStyles.AllowLeadingSign,
                CultureInfo.InvariantCulture,
                out _
            );

        /// <summary>
        /// Validates whether this string is a valid Double with invariant culture.
        /// </summary>
        /// <param name="str">The string to validate.</param>
        /// <returns>True if this string is a valid Double.</returns>
        public static bool IsValidDouble(this string str) =>
            double.TryParse(
                str,
                NumberStyles.AllowDecimalPoint |
                NumberStyles.AllowLeadingSign,
                CultureInfo.InvariantCulture,
                out _
            );

        /// <summary>
        /// Validates whether this string is a valid Float with invariant culture.
        /// </summary>
        /// <param name="str">The string to validate.</param>
        /// <returns>True if this string is a valid Float.</returns>
        public static bool IsValidFloat(this string str) =>
            float.TryParse(
                str,
                NumberStyles.AllowDecimalPoint |
                NumberStyles.AllowLeadingSign,
                CultureInfo.InvariantCulture,
                out _
            );

        /// <summary>
        /// Validates whether this string is a valid Int with invariant culture.
        /// </summary>
        /// <param name="str">The string to validate.</param>
        /// <returns>True if this string is a valid Int.</returns>
        public static bool IsValidInt(this string str) =>
            int.TryParse(
                str,
                NumberStyles.AllowDecimalPoint |
                NumberStyles.AllowLeadingSign,
                CultureInfo.InvariantCulture,
                out _
            );

        /// <summary>
        /// Validates whether this string is a valid url.
        /// </summary>
        /// <param name="str">The string to validate.</param>
        /// <returns>True if this string is a valid Url.</returns>
        public static bool IsValidUrl(this string str) =>
            Uri.TryCreate(str, UriKind.Absolute, out var uriResult) &&
            (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

        /// <summary>
        /// Validates whether this string is a valid DateTime with invariant culture.
        /// </summary>
        /// <param name="str">The string to validate.</param>
        /// <returns>True if this string is a valid DateTime with invariant culture.</returns>
        public static bool IsValidDate(this string str) =>
            DateTime.TryParse(
                str,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out _
            );

        /// <summary>
        /// Validates wheter this string has a valid email format.
        /// </summary>
        /// <param name="str">The string to validate.</param>
        /// <returns>True if this string has a valid email format.</returns>
        public static bool IsValidEmail(this string str) =>
            new Regex(EmailRegex).IsMatch(str);

        /// <summary>
        /// Validates wheter this string has a valid name format.
        /// </summary>
        /// <param name="str">The string to validate.</param>
        /// <returns>True if this string has a valid name format.</returns>
        public static bool IsValidName(this string str) =>
            new Regex(NameRegex).IsMatch(str);

        /// <summary>
        /// Validates wheter this string has a valid phone number.
        /// </summary>
        /// <param name="str">The string to validate.</param>
        /// <returns>True if this string has a valid phone number.</returns>
        public static bool IsValidPhoneNumber(this string str) =>
            new Regex(PhoneRegex).IsMatch(str);

        /// <summary>
        /// Validates wheter this string has a valid zip code.
        /// </summary>
        /// <param name="str">The string to validate.</param>
        /// <returns>True if this string has a valid phone number.</returns>
        public static bool IsValidZip(this string str) =>
            new Regex(ZipRegex).IsMatch(str);
    }
}
