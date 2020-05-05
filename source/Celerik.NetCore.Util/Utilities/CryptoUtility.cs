using CryptoHelper;

namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Provides cryptography utilities.
    /// </summary>
    public static class CryptoUtility
    {
        /// <summary>
        /// Returns a hashed representation of the specified password.
        /// </summary>
        /// <param name="password">The password to generate a hash value for.</param>
        /// <returns>The hash value for password as a base-64-encoded string.</returns>
        /// <exception cref="System.ArgumentNullException">Password is null.</exception>
        public static string HashPassword(string password)
            => Crypto.HashPassword(password);

        /// <summary>
        /// Determines whether the specified RFC 2898 hash and password are a
        /// cryptographic match.
        /// </summary>
        /// <param name="hash">The previously-computed RFC 2898 hash value as a
        /// base-64-encoded string.</param>
        /// <param name="password">The plaintext password to cryptographically
        /// compare with hashedPassword.</param>
        /// <returns>True if the hash value is a cryptographic match for the
        /// password; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">HashedPassword or password
        /// are null.</exception>
        public static bool VerifyHashedPassword(string hash, string password)
            => Crypto.VerifyHashedPassword(hash, password);
    }
}
