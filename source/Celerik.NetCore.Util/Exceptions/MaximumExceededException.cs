﻿using System;

namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Represents errors that occur when a maximum value is exceeded.
    /// </summary>
    public class MaximumExceededException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="paramName">The name of the parameter that
        /// caused the current exception.</param>
        /// <param name="maximum">The maximum value allowed.</param>
        /// <param name="actual">The actual value.</param>
        public MaximumExceededException(string paramName, object maximum, object actual)
            : this(UtilResources.Get("MaximumExceededException", paramName, maximum, actual))
        {
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public MaximumExceededException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public MaximumExceededException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of
        /// the current exception, or a null reference if no inner exception
        /// is specified.</param>
        public MaximumExceededException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
