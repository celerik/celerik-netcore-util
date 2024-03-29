﻿using System;

namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Represents errors that occur when a required property is
    /// null or empty.
    /// </summary>
    public class PropNullException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public PropNullException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="propName">The name of the parameter that
        /// caused the current exception.</param>
        public PropNullException(string propName)
            : this(propName, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="propName">The name of the parameter that
        /// caused the current exception.</param>
        /// <param name="innerException">The exception that is the cause of
        /// the current exception, or a null reference if no inner exception
        /// is specified.</param>
        public PropNullException(string propName, Exception innerException)
            : base(UtilResources.Get("PropNullException", propName), innerException)
        {
        }
    }
}
