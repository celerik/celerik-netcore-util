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
        /// specified assembly.
        /// </summary>
        /// <param name="fileName">The case-sensitive name of the manifest resource
        /// being requested without including the assembly namespace.
        /// E.g.: "FolderName.FileName.extension".</param>
        /// <param name="assembly">The assembly where we get the resource. If
        /// null, we search the resource into the calling assembly.</param>
        /// <returns>Content of the embedded file existing into the
        /// specified assembly.</returns>
        /// <exception cref="FileNotFoundException">The embedded file could not
        /// be found.</exception>
        public static string ReadFile(string fileName, Assembly assembly = null)
        {
            try
            {
                if (assembly == null)
                    assembly = Assembly.GetCallingAssembly();

                var namespce = assembly.FullName.Split(',')[0];
                fileName = $"{namespce}.{fileName}";

                using var stream = assembly.GetManifestResourceStream(fileName);
                using var reader = new StreamReader(stream);

                var content = reader.ReadToEnd();
                return content;
            }
            catch
            {
                var msg = UtilResources.Get("ErrorReadingEmbeddedFile");
                throw new FileNotFoundException(msg, fileName);
            }
        }

        /// <summary>
        /// Reads the content of an embedded JSON file into the specified
        /// output type.
        /// </summary>
        /// <typeparam name="TOutputType">The type to which the JSON file will be
        /// loaded.</typeparam>
        /// <param name="fileName">The case-sensitive name of the manifest resource
        /// being requested without including the assembly namespace.
        /// E.g.: "FolderName.FileName.json".</param>
        /// <param name="assembly">The assembly where we get the resource. If
        /// null, we search the resource into the calling assembly.</param>
        /// <param name="serializeAll">Indicates whether all props should be
        /// serialized without taking into account the JsonIgnore attribute.</param>
        /// <returns>Content of the embedded JSON file loaded into the
        /// specified Type.</returns>
        /// <exception cref="FileNotFoundException">The embedded JSON file could not
        /// be found.</exception>
        /// <exception cref="FileLoadException">There was an error when trying to
        /// load the content of the JSON file into the specified output type.
        /// </exception>
        public static TOutputType ReadJson<TOutputType>(
            string fileName, Assembly assembly = null, bool serializeAll = true)
        {
            if (assembly == null)
                assembly = Assembly.GetCallingAssembly();

            var content = ReadFile(fileName, assembly);

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
                var msg = UtilResources.Get("ErrorLoadingEmbeddedJsonFile", typeof(TOutputType));
                throw new FileLoadException(msg, fileName);
            }
        }
    }
}
