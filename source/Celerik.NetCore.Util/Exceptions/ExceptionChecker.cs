using System;

namespace Celerik.NetCore.Util
{
    /// <summary>
    /// 
    /// </summary>
    public static class ExceptionChecker
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpStatusCode"></param>
        public static void AssertHttp200Code(int httpStatusCode)
        {
            if (httpStatusCode < 200 || httpStatusCode > 299)
                throw new Exception($"Expecting a 2xx HttpStatusCode but received: '{httpStatusCode}'");
        }
    }
}
