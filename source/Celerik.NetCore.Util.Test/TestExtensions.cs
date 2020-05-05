using System;

namespace Celerik.NetCore.Util.Test
{
    public static class TestExtensions
    {
        public static bool ContainsInvariant(this string str, string value)
            => str == null
                ? throw new ArgumentNullException(nameof(str))
                : str.Contains(value, StringComparison.InvariantCulture);
    }
}
