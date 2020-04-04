using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Implementation of the IConfiguration interface for testing purposes. With
    /// this implementation we retrieve and store config values in memory.
    /// </summary>
    public class DummyIConfiguration : IConfiguration
    {
        /// <summary>
        /// Object to store configuration values in memory.
        /// </summary>
        private readonly IDictionary<string, string> _values = new Dictionary<string, string>();

        /// <summary>
        /// Gets or sets a configuration value.
        /// </summary>
        /// <param name="key">The configuration key.</param>
        /// <returns>The configuration value.</returns>
        public string this[string key]
        {
            get => _values.ContainsKey(key) ? _values[key] : null;
            set => _values[key] = value;
        }

        /// <summary>
        /// Gets the immediate descendant configuration sub-sections.
        /// </summary>
        /// <returns>The configuration sub-sections.</returns>
        public IEnumerable<IConfigurationSection> GetChildren()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a Microsoft.Extensions.Primitives.IChangeToken that can be used to observe
        /// when this configuration is reloaded.
        /// </summary>
        /// <returns>A Microsoft.Extensions.Primitives.IChangeToken.</returns>
        public IChangeToken GetReloadToken()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a configuration sub-section with the specified key.
        /// </summary>
        /// <param name="key">The key of the configuration section.</param>
        /// <returns>The Microsoft.Extensions.Configuration.IConfigurationSection.</returns>
        public IConfigurationSection GetSection(string key)
        {
            throw new NotImplementedException();
        }
    }
}
