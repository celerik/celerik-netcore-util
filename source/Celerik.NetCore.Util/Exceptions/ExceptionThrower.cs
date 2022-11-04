using System;

namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Utility to throw exceptions when a condition is matched.
    /// </summary>
    public static class ExceptionThrower
    {
        /// <summary>
        /// Ensures that the passed-in http status code is a 200 response (200-299),
        /// if not, an exception is thrown.
        /// </summary>
        /// <param name="httpStatusCode">The status code to check.</param>
        /// <exception cref="Exception">If the status code is not a 200 response (200-299).
        /// </exception>
        public static void AssertHttp200Code(int httpStatusCode)
        {
            if (httpStatusCode < 200 || httpStatusCode > 299)
                throw new Exception(UtilResources.Get("AssertHttp200Code", httpStatusCode));
        }
    }
}
