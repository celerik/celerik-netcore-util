using System;

namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Represents errors that occur when a minimum value is subceeded.
    /// </summary>
    public class MinimunSubceededException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="paramName">The name of the parameter that
        /// caused the current exception.</param>
        /// <param name="minimun">The minimum value allowed.</param>
        /// <param name="actual">The actual value.</param>
        public MinimunSubceededException(string paramName, object minimun, object actual)
            : this(UtilResources.Get("MinimunSubceededException", paramName, minimun, actual))
        {
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public MinimunSubceededException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public MinimunSubceededException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of
        /// the current exception, or a null reference if no inner exception
        /// is specified.</param>
        public MinimunSubceededException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
