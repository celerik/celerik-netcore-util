using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Provides embedded file utilities.
    /// </summary>
    public static class EmbeddedFileUtility
    {
        /// <summary>
        /// Reads the content of an embedded file existing into the
        /// passed-in assembly.
        /// </summary>
        /// <param name="name">The case-sensitive name of the manifest resource
        /// being requested without including the assembly namespace.
        /// E.g.: "FolderName.FileName.extension".</param>
        /// <param name="assembly">The assembly where we get the resource. If
        /// null, we search the resource into the calling assembly.</param>
        /// <returns>Content of the embedded file existing into the
        /// passed-in assembly.</returns>
        /// <exception cref="FileNotFoundException">If the embedded file could not
        /// be found.</exception>
        public static string ReadFile(string name, Assembly assembly = null)
        {
            try
            {
                if (assembly == null)
                    assembly = Assembly.GetCallingAssembly();

                var namespce = assembly.FullName.Split(',')[0];
                var fullPath = $"{namespce}.{name}";

                using var stream = assembly.GetManifestResourceStream(fullPath);
                using var reader = new StreamReader(stream);
                var content = reader.ReadToEnd();

                return content;
            }
            catch
            {
                throw new FileNotFoundException(UtilResources.Get(
                    "EmbeddedFileUtility.ReadFileException", name
                ));
            }
        }

        /// <summary>
        /// Reads the content of an embedded JSON file existing into the
        /// calling assembly.
        /// </summary>
        /// <typeparam name="TOutputType">The type to which the JSON file will be
        /// converted.</typeparam>
        /// <param name="name">The case-sensitive name of the manifest resource
        /// being requested without including the assembly namespace.
        /// E.g.: "FolderName.FileName.json".</param>
        /// <param name="serializeAll">Indicates whether all props should be
        /// serialized without taking into account the JsonIgnore attribute.</param>
        /// <returns>Content of the embedded JSON file existing into the
        /// calling assembly, casted to the specified Type.</returns>
        /// <exception cref="FileNotFoundException">If the embedded JSON file could not
        /// be found.</exception>
        /// <exception cref="FileLoadException">If there was an error trying to load
        /// the content of the JSON file.</exception>
        public static TOutputType ReadJson<TOutputType>(string name, bool serializeAll = true)
        {
            var content = ReadFile(name, Assembly.GetCallingAssembly());

            try
            {
                if (serializeAll)
                {
                    var settings = new JsonSerializerSettings
                    {
                        ContractResolver = new SerializeAllContractResolver()
                    };

                    var obj = JsonConvert.DeserializeObject<TOutputType>(content, settings);
                    return obj;
                }
                else
                {
                    var obj = JsonConvert.DeserializeObject<TOutputType>(content);
                    return obj;
                }
            }
            catch
            {
                throw new FileLoadException(UtilResources.Get(
                    "EmbeddedFileUtility.ReadJsonException", name, typeof(TOutputType)
                ));
            }
        }
    }
}
