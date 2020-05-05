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
        /// Gets the full path of the executing assembly directory.
        /// </summary>
        /// <returns>Full path of the executing assembly directory.</returns>
        public static string GetExecutingAssemblyDir()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var codeBase = assembly.CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            var directory = Path.GetDirectoryName(path);

            return directory;
        }

        /// <summary>
        /// Gets the full path of a file that is placed in the executing
        /// assembly directory.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <returns>Full path of a file that is placed in the executing
        /// assembly directory.</returns>
        public static string LocateFileInExecutingAssemblyDir(string fileName)
            => $"{GetExecutingAssemblyDir()}{Path.DirectorySeparatorChar}{fileName}";

        /// <summary>
        /// Checks if the passed-in fileName exists in the executing
        /// assembly directory.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <returns>True if the passed-in fileName exists in the executing
        /// assembly directory.</returns>
        public static bool ExistsFileInExecutingAssemblyDir(string fileName)
            => File.Exists(LocateFileInExecutingAssemblyDir(fileName));
    }
}
