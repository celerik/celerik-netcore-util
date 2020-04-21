using System;
using Microsoft.Extensions.Configuration;

namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Provides configuration utilities.
    /// </summary>
    public static class ConfigUtility
    {
        /// <summary>
        /// Attempts to create a T instance by matching property names against
        /// configuration keys recursively.
        /// </summary>
        /// <typeparam name="T">The type of the instance to be created.</typeparam>
        /// <param name="config">Reference to the current IConfiguration instance.
        /// </param>
        /// <param name="key">The key of the configuration section.</param>
        /// <returns>Instance of type T.</returns>
        public static T Read<T>(this IConfiguration config, string key)
        {
            if (config == null)
                throw new ArgumentNullException(
                    UtilResources.Get("Common.ArgumentCanNotBeNull", nameof(config)));

            var configSection = config.GetSection(key);
            if (!configSection.Exists())
                throw new ConfigException(
                    UtilResources.Get("Common.ConfigurationKeyNotFound", key)
                );

            var instance = (T)Activator.CreateInstance(typeof(T));
            configSection.Bind(instance);

            return instance;
        }
    }
}
