using System;

namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Defines a custom Code attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum)]
    public class CodeAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="code">Code value.</param>
        public CodeAttribute(string code)
        {
            Code = code;
        }

        /// <summary>
        /// Gets or sets the Code value.
        /// </summary>
        public string Code { get; private set; }
    }
}
