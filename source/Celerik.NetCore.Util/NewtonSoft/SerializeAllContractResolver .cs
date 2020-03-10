using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Contract resolver to serialize all props in an object not taking into
    /// account the JsonIgnore attribute.
    /// </summary>
    /// <example>
    ///     <code>
    ///         var settings = new JsonSerializerSettings
    ///         {
    ///             ContractResolver = new SerializeAllContractResolver()
    ///         };
    ///
    ///         var json = JsonConvert.SerializeObject(obj, settings);
    ///     </code>
    /// </example>
    public class SerializeAllContractResolver : DefaultContractResolver
    {
        /// <summary>
        /// Creates properties for the given Newtonsoft.Json.Serialization.JsonContract.
        /// </summary>
        /// <param name="type">The type to create properties for.</param>
        /// <param name="memberSerialization">The member serialization mode for the type.
        /// </param>
        /// <returns>Properties for the given Newtonsoft.Json.Serialization.JsonContract.
        /// </returns>
        protected override IList<JsonProperty> CreateProperties(
            Type type, MemberSerialization memberSerialization)
        {
            var props = base.CreateProperties(type, memberSerialization);

            foreach (var prop in props)
                prop.Ignored = false;

            return props;
        }
    }
}
