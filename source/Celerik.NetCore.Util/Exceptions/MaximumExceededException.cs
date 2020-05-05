using System;

namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Represents errors that occur when a required argument is
    /// null or empty.
    /// </summary>
    public class MaximumExceedException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="minimun"></param>
        /// <param name="actual"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public MaximumExceedException(string paramName, object minimun, object actual)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public MaximumExceedException()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public MaximumExceedException(string message) : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public MaximumExceedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
