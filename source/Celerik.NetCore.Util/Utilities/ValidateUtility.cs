using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using PhoneNumbers;

namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Provides validation utilities.
    /// </summary>
    public static class ValidateUtility
    {
        /// <summary>
        /// Regex for emails.
        /// </summary>
        public const string EmailRegex = @"^(([^<>()\[\]\\.,;:\s@""]+(\.[^<>()\[\]\\.,;:\s@""]+)*)|("".+""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$";

        /// <summary>
        /// Regex for names (only letters and spaces are allowed).
        /// </summary>
        public const string PersonNameRegex = @"^[A-zÀ-ú]+(([',. -][A-zÀ-ú ])?[A-zÀ-ú]*)*$";

        /// <summary>
        /// Regex for standard passwords. They should be between 8-50
        /// characteres and should include an upper letter, a lower
        /// letter, a number and a symbol: #$^+=!*()@%&amp;
        /// </summary>
        public const string StandardPasswordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,50}$";

        /// <summary>
        /// Regex for ZIP codes (U.S. postal code), allowing both the
        /// five-digit and nine-digit (called ZIP+4) formats. 
        /// </summary>
        public const string ZipPlus4Regex = @"^[0-9]{5}(?:-[0-9]{4})?$";

        /// <summary>
        /// Regex for database table names.
        /// </summary>
        public const string TableNameRegex = @"^[A-Za-z][A-Za-z0-9]{2,62}$";

        /// <summary>
        /// Validates wheter this string has a valid email format.
        /// </summary>
        /// <param name="str">The string to validate.</param>
        /// <returns>True if this string has a valid email format.</returns>
        public static bool IsValidEmail(this string str)
            => new Regex(EmailRegex).IsMatch(str ?? "");

        /// <summary>
        /// Validates wheter this string has a valid name format
        /// (only letters and spaces are allowed).
        /// </summary>
        /// <param name="str">The string to validate.</param>
        /// <returns>True if this string has a valid name format.</returns>
        public static bool IsValidPersonName(this string str)
            => new Regex(PersonNameRegex).IsMatch(str ?? "");

        /// <summary>
        /// Validates wheter this string is a valid standard password.
        /// It should be between 8-50 characteres and should include
        /// an upper letter, a lower letter, a number and a symbol:
        /// #$^+=!*()@%&amp;
        /// </summary>
        /// <param name="str">The string to validate.</param>
        /// <returns>True if this string is a valid password.</returns>
        public static bool IsValidStandardPassword(this string str)
            => new Regex(StandardPasswordRegex).IsMatch(str ?? "");

        /// <summary>
        /// Validates wheter this string is a valid zip code (U.S. postal
        /// code), allowing both the five-digit and nine-digit (called ZIP+4)
        /// formats. 
        /// </summary>
        /// <param name="str">The string to validate.</param>
        /// <returns>True if this string is a valid zip code.</returns>
        public static bool IsValidZipPlus4(this string str)
            => new Regex(ZipPlus4Regex).IsMatch(str ?? "");

        /// <summary>
        /// Validates wheter this string is a valid databse table name.
        /// </summary>
        /// <param name="str">The string to validate.</param>
        /// <returns>True if this string is a valid databse table name.</returns>
        public static bool IsValidTableName(this string str)
            => new Regex(TableNameRegex).IsMatch(str ?? "");

        /// <summary>
        /// Validates whether this string is a valid boolean. The
        /// following are the valid boolean values: "true", "True",
        /// "TRUE", "false", "False", "FALSE". 
        /// </summary>
        /// <param name="str">The string to validate.</param>
        /// <returns>True if this string is a valid boolean.</returns>
        public static bool IsValidBool(this string str)
            => new string[] { "true", "True", "TRUE", "false", "False", "FALSE" }.Contains(str);

        /// <summary>
        /// Validates whether this string is a valid url.
        /// </summary>
        /// <param name="str">The string to validate.</param>
        /// <returns>True if this string is a valid Url.</returns>
        public static bool IsValidUrl(this string str)
            => Uri.TryCreate(str, UriKind.Absolute, out var uriResult) &&
                (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

        /// <summary>
        /// Validates wheter this string is a valid international
        /// phone number E.164 compilant.
        /// </summary>
        /// <param name="str">The string to validate.</param>
        /// <returns>True this string is a valid phone number
        /// E.164 compilant.</returns>
        [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Any exception indicates the number is invalid")]
        public static bool IsValidInternationalPhone(this string str)
        {
            var isValid = false;

            try
            {
                var phoneUtil = PhoneNumberUtil.GetInstance();
                var phoneNumber = phoneUtil.Parse(str, null);
                isValid = phoneUtil.IsValidNumber(phoneNumber);
            }
            catch { }

            return isValid;
        }

        /// <summary>
        /// Validates whether this string is a valid Int with
        /// invariant culture.
        /// </summary>
        /// <param name="str">The string to validate.</param>
        /// <returns>True if this string is a valid Int.</returns>
        public static bool IsValidIntInvariant(this string str)
            => int.TryParse(
                str,
                NumberStyles.AllowDecimalPoint |
                NumberStyles.AllowLeadingSign,
                CultureInfo.InvariantCulture,
                out _
            );

        /// <summary>
        /// Validates whether this string is a valid Float with
        /// invariant culture.
        /// </summary>
        /// <param name="str">The string to validate.</param>
        /// <returns>True if this string is a valid Float.</returns>
        public static bool IsValidFloatInvariant(this string str) 
            => float.TryParse(
                str,
                NumberStyles.AllowDecimalPoint |
                NumberStyles.AllowLeadingSign,
                CultureInfo.InvariantCulture,
                out _
            );

        /// <summary>
        /// Validates whether this string is a valid Double with
        /// invariant culture.
        /// </summary>
        /// <param name="str">The string to validate.</param>
        /// <returns>True if this string is a valid Double.</returns>
        public static bool IsValidDoubleInvariant(this string str) 
            => double.TryParse(
                str,
                NumberStyles.AllowDecimalPoint |
                NumberStyles.AllowLeadingSign,
                CultureInfo.InvariantCulture,
                out _
            );

        /// <summary>
        /// Validates whether this string is a valid Decimal with
        /// invariant culture.
        /// </summary>
        /// <param name="str">The string to validate.</param>
        /// <returns>True if this string is a valid Decimal.</returns>
        public static bool IsValidDecimalInvariant(this string str) 
            => decimal.TryParse(
                str,
                NumberStyles.AllowDecimalPoint |
                NumberStyles.AllowLeadingSign,
                CultureInfo.InvariantCulture,
                out _
            );

        /// <summary>
        /// Validates whether this string is a valid DateTime with
        /// invariant culture.
        /// </summary>
        /// <param name="str">The string to validate.</param>
        /// <returns>True if this string is a valid DateTime with invariant culture.</returns>
        public static bool IsValidDateInvariant(this string str)
            => DateTime.TryParse(
                str,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out _
            );
    }
}
