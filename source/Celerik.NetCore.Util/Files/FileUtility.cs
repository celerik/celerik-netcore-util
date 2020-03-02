using System;
using System.IO;
using System.Reflection;

namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Provides file utilities.
    /// </summary>
    public static class FileUtility
    {
        /// <summary>
        /// Gets the full path of the current assembly.
        /// </summary>
        /// <returns>Full path of the current assmbly.</returns>
        public static string GetAssemblyDirectory()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var codeBase = assembly.CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            var directory = Path.GetDirectoryName(path);

            return directory;
        }
    }
}
