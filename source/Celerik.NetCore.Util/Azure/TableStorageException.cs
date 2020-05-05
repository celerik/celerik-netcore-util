using System;

namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Represents errors that occur when performing Table Storage
    /// operations.
    /// </summary>
    public class TableStorageException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="operationName">The name of the operation that failed.
        /// </param>
        /// <param name="innerException">The exception that is the cause of
        /// the current exception.</param>
        public TableStorageException(string operationName, Exception innerException)
            : base(UtilResources.Get("TableStorageException", operationName, innerException.Message), innerException)
        {
        }
    }
}
