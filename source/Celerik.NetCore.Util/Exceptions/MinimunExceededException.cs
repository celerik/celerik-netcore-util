using System;

namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Represents errors that occur when a required argument is
    /// null or empty.
    /// </summary>
    public class MinimunExceedException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="minimun"></param>
        /// <param name="actual"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public MinimunExceedException(string paramName, object minimun, object actual)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public MinimunExceedException()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public MinimunExceedException(string message) : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public MinimunExceedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
