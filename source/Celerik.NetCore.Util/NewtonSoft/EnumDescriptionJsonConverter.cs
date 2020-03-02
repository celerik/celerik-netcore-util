using System;
using Newtonsoft.Json;

namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Serializes an Enum value to its corresponding DescriptionAttribute.
    /// </summary>
    /// <code>
    ///     enum MyEnum
    ///     {
    ///         [Description("Some Value")]
    ///         SomeValue
    ///     }
    ///     class MyClass
    ///     {
    ///         [JsonConverter(typeof(EnumDescriptionJsonConverter))]
    ///         public MyEnum MyProp { get; set; }
    ///     }
    /// </code>
    public class EnumDescriptionJsonConverter : JsonConverter
    {
        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The Newtonsoft.Json.JsonWriter to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (writer == null)
                throw new ArgumentException(UtilResources.Get("EnumDescriptionJsonConverter.WriteJson.NullWriter"));

            writer.WriteValue(EnumUtility.GetDescription((Enum)value));
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The Newtonsoft.Json.JsonReader to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return null;
        }

        /// <summary>
        /// Gets a value indicating whether this Newtonsoft.Json.JsonConverter can read JSON.
        /// </summary>
        public override bool CanRead
        {
            get { return false; }
        }

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>true if this instance can convert the specified object type;
        /// otherwise, false.</returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Enum);
        }
    }
}
