using System;
using System.Runtime.Serialization;

namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Exception to indicate an invalid value was read from the config file.
    /// </summary>
    [Serializable]
    public class ConfigFileException : Exception
    {
        /// <summary>
        /// Initializes a new ConfigFileException.
        /// </summary>
        public ConfigFileException()
        {
        }

        /// <summary>
        /// Initializes a new ConfigFileException.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ConfigFileException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new ConfigFileException.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current
        /// exception, or a null reference if no inner exception is specified.</param>
        public ConfigFileException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new ConfigFileException.
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo that
        /// holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The StreamingContext that contains contextual
        /// information about the source or destination.</param>
        protected ConfigFileException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
