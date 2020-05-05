using System;

namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Represents errors that occur when a required argument is
    /// null or empty.
    /// </summary>
    public class ArgumentNullOrEmptyException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ArgumentNullOrEmptyException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="paramName">The name of the parameter that
        /// caused the current exception.</param>
        public ArgumentNullOrEmptyException(string paramName)
            : this(paramName, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="paramName">The name of the parameter that
        /// caused the current exception.</param>
        /// <param name="innerException">The exception that is the cause of
        /// the current exception, or a null reference if no inner exception
        /// is specified.</param>
        public ArgumentNullOrEmptyException(string paramName, Exception innerException)
            : base(UtilResources.Get("ArgumentNullOrEmptyException", paramName), innerException)
        {
        }
    }
}
