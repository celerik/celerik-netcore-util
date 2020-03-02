using System;
using System.Diagnostics;

namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Provides stack trace utilities.
    /// </summary>
    public static class StackTraceUtility
    {
        /// <summary>
        /// Gets the top method name of this stackTrace, in the format: Class.Method().
        /// </summary>
        /// <param name="stackTrace">Stack trace to retrieve the top method name.</param>
        /// <returns>Top method name of this stackTrace.</returns>
        public static string GetMethodName(this StackTrace stackTrace)
        {
            if (stackTrace == null)
                throw new ArgumentException(UtilResources.Get("StackTraceUtility.GetMethodName.NullStackTrace"));

            var toString = stackTrace.ToString();
            var firstLine = toString.Substring(0, toString.IndexOf(Environment.NewLine, StringComparison.InvariantCulture));
            var tokens = firstLine.Split('.');

            return $"{tokens[^2]}.{tokens[^1]}";
        }
    }
}
