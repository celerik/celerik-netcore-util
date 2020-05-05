using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using Newtonsoft.Json;

namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Provides enum utilities.
    /// </summary>
    public static class EnumUtility
    {
        /// <summary>
        /// Gets an attribute belonging to this enumeration. Returns null if the
        /// attribute does not exists.
        /// </summary>
        /// <typeparam name="TAttribute">The attribute type to get.</typeparam>
        /// <param name="value">Enum value.</param>
        /// <returns>Attribute belonging to this enumeration.</returns>
        /// <exception cref="ArgumentNullException">Enum value is null.</exception>
        public static TAttribute GetAttribute<TAttribute>(this Enum value)
            where TAttribute : Attribute
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var type = typeof(TAttribute);
            var memberInfo = value.GetType().GetMember(value.ToString());

            var attribute = memberInfo[0].GetCustomAttributes(type, inherit: false)
                .OfType<TAttribute>().FirstOrDefault();

            return attribute;
        }

        /// <summary>
        /// Gets the CodeAttribute value of this enumeration.
        /// </summary>
        /// <param name="value">Enum value.</param>
        /// <param name="defaultVal">Value to return by default in case the enumeration
        /// does not have a CodeAttribute.</param>
        /// <returns>Code of this enumeration.</returns>
        public static string GetCode(this Enum value, string defaultVal = null)
            => value.GetAttribute<CodeAttribute>()?.Code
                ?? defaultVal;

        /// <summary>
        /// Gets the CodeAttribute value of an enumeration matching the
        /// passed-in value.
        /// </summary>
        /// <typeparam name="TEnum">Enum type.</typeparam>
        /// <param name="value">Enum value.</param>
        /// <param name="defaultVal">Value to return by default in case the value doesn't
        /// exist in the enum.</param>
        /// <returns>Code of the enumeration.</returns>
        /// <exception cref="InvalidCastException">TEnum is not an enum.</exception>
        public static string GetCode<TEnum>(int value, string defaultVal = null)
            where TEnum : struct, IConvertible
        {
            var type = typeof(TEnum);

            if (!type.IsEnum)
                throw new InvalidCastException(
                    UtilResources.Get("TypeIsNotAnEnum", type));
            if (!Enum.IsDefined(typeof(TEnum), value))
                return defaultVal;

            var code = ((Enum)Enum.ToObject(type, value)).GetCode(defaultVal);
            return code;
        }

        /// <summary>
        /// Gets the DescriptionAttribute value of this enumeration. If the
        /// enumeration does not have a DescriptionAttribute, the enum name is
        /// returned.
        /// </summary>
        /// <param name="value">Enum value.</param>
        /// <returns>Description of this enumeration.</returns>
        public static string GetDescription(this Enum value)
            => value.GetAttribute<DescriptionAttribute>()?.Description
                ?? value.ToString();

        /// <summary>
        /// Gets the DescriptionAttribute value of an enumeration matching the
        /// passed-in value.
        /// </summary>
        /// <typeparam name="TEnum">Enum type.</typeparam>
        /// <param name="value">Enum value.</param>
        /// <param name="defaultVal">Value to return by default in case the value doesn't
        /// exist in the enum.</param>
        /// <returns>Description of the enumeration.</returns>
        /// <exception cref="InvalidCastException">TEnum is not an enum.</exception>
        public static string GetDescription<TEnum>(int value, string defaultVal = null)
            where TEnum : struct, IConvertible
        {
            var type = typeof(TEnum);

            if (!type.IsEnum)
                throw new InvalidCastException(
                    UtilResources.Get("TypeIsNotAnEnum", type));
            if (!Enum.IsDefined(typeof(TEnum), value))
                return defaultVal;

            var description = ((Enum)Enum.ToObject(type, value)).GetDescription();
            return description;
        }

        /// <summary>
        /// Gets an enum value from its CodeAttribute.
        /// </summary>
        /// <typeparam name="TEnum">Enum type.</typeparam>
        /// <param name="code">Code of the enum value to search.</param>
        /// <returns>Enum value for the given CodeAttribute.</returns>
        /// <param name="defaultVal">Value to return by default in case the code
        /// doesn't exist in the enum.</param>
        /// <exception cref="InvalidCastException">TEnum is not an enum.</exception>
        public static TEnum GetValueFromCode<TEnum>(string code, TEnum defaultVal = default)
            where TEnum : struct, IConvertible
        {
            var type = typeof(TEnum);

            if (!type.IsEnum)
                throw new InvalidCastException(
                    UtilResources.Get("TypeIsNotAnEnum", type));

            foreach (var field in type.GetFields())
            {
                if (Attribute.GetCustomAttribute(
                    field, typeof(CodeAttribute)
                ) is CodeAttribute attribute)
                {
                    if (attribute.Code == code)
                        return (TEnum)field.GetValue(null);
                }
            }

            return defaultVal;
        }

        /// <summary>
        /// Gets an enum value from its DescriptionAttribute.
        /// </summary>
        /// <typeparam name="TEnum">Enum type.</typeparam>
        /// <param name="description">Description of the enum value to search.</param>
        /// <returns>Enum value for the given DescriptionAttribute.</returns>
        /// <param name="defaultVal">Value to return by default in case the description
        /// doesn't exist in the enum.</param>
        /// <exception cref="InvalidCastException">TEnum is not an enum.</exception>
        public static TEnum GetValueFromDescription<TEnum>(string description, TEnum defaultVal = default)
            where TEnum : struct, IConvertible
        {
            var type = typeof(TEnum);

            if (!type.IsEnum)
                throw new InvalidCastException(
                    UtilResources.Get("TypeIsNotAnEnum", type));

            foreach (var field in type.GetFields())
            {
                if (Attribute.GetCustomAttribute(
                    field, typeof(DescriptionAttribute)
                ) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description)
                        return (TEnum)field.GetValue(null);
                }

                if (field.Name == description)
                    return (TEnum)field.GetValue(null);
            }

            return defaultVal;
        }

        /// <summary>
        /// Gets the minimun value of an enum.
        /// </summary>
        /// <typeparam name="TEnum">Enum type.</typeparam>
        /// <returns>Minimun value of the enum.</returns>
        /// <exception cref="InvalidCastException">TEnum is not an enum.</exception>
        public static int GetMin<TEnum>()
            where TEnum : struct, IConvertible
        {
            var type = typeof(TEnum);

            if (!type.IsEnum)
                throw new InvalidCastException(
                    UtilResources.Get("TypeIsNotAnEnum", type));

            var min = Enum.GetValues(typeof(TEnum)).Cast<int>().Min();
            return min;
        }

        /// <summary>
        /// Gets the maximun value of an enum.
        /// </summary>
        /// <typeparam name="TEnum">Enum type.</typeparam>
        /// <returns>Maximun value of the enum.</returns>
        /// <exception cref="InvalidCastException">TEnum is not an enum.</exception>
        public static int GetMax<TEnum>()
            where TEnum : struct, IConvertible
        {
            var type = typeof(TEnum);

            if (!type.IsEnum)
                throw new InvalidCastException(
                    UtilResources.Get("TypeIsNotAnEnum", type));

            var max = Enum.GetValues(typeof(TEnum)).Cast<int>().Max();
            return max;
        }

        /// <summary>
        /// Converts an enum to a list of strings. Description of each enum
        /// item is based on the DescriptionAttribute, if the enum item does not
        /// have a DescriptionAttribute, the enum name is took.
        /// </summary>
        /// <typeparam name="TEnum">Enum type.</typeparam>
        /// <returns>Enumeration converted to a list of strings.</returns>
        /// <exception cref="InvalidCastException">TEnum is not an enum.</exception>
        public static List<string> ToList<TEnum>() where TEnum : struct, IConvertible
        {
            var type = typeof(TEnum);

            if (!type.IsEnum)
                throw new InvalidCastException(
                    UtilResources.Get("TypeIsNotAnEnum", type));

            var list = new List<string>();
            var fields = type.GetFields().OrderBy(field => field.MetadataToken);

            foreach (var field in fields)
            {
                if (field.IsStatic)
                {
                    var description = Attribute.GetCustomAttribute(
                        field, typeof(DescriptionAttribute)
                    ) is DescriptionAttribute descAttribute
                        ? descAttribute.Description
                        : field.Name;

                    list.Add(description);
                }
            }

            return list;
        }

        /// <summary>
        /// Converts an enum to a list of objects.
        /// </summary>
        /// <typeparam name="TEnum">Enum type.</typeparam>
        /// <typeparam name="TList">List type.</typeparam>
        /// <param name="valueProp">Property name for the vale in the list.</param>
        /// <param name="descriptionProp">Property name for the description in the list.</param>
        /// <param name="codeProp">Property name for the code in the list.</param>
        /// <returns>Enumeration converted to a list of objects.</returns>
        /// <exception cref="InvalidCastException">TEnum is not an enum.</exception>
        public static List<TList> ToList<TEnum, TList>(
            string valueProp,
            string descriptionProp,
            string codeProp = null)
                where TEnum : struct, IConvertible
                where TList : class
        {
            var type = typeof(TEnum);

            if (!type.IsEnum)
                throw new InvalidCastException(
                    UtilResources.Get("TypeIsNotAnEnum", type));

            var list = new List<TList>();
            var fields = type.GetFields().OrderBy(field => field.MetadataToken);

            foreach (var field in fields)
            {
                if (field.IsStatic)
                {
                    dynamic expando = new ExpandoObject();
                    var dictionary = expando as IDictionary<string, object>;

                    dictionary[valueProp] = (int)field.GetValue(null);

                    dictionary[descriptionProp] = Attribute.GetCustomAttribute(
                            field, typeof(DescriptionAttribute)
                        ) is DescriptionAttribute descAttribute
                            ? descAttribute.Description
                            : field.Name;

                    if (codeProp != null)
                    {
                        dictionary[codeProp] = Attribute.GetCustomAttribute(
                            field, typeof(CodeAttribute)
                        ) is CodeAttribute codeAttribute
                            ? codeAttribute.Code
                            : null;
                    }

                    var json = JsonConvert.SerializeObject(expando);
                    var obj = JsonConvert.DeserializeObject<TList>(json);

                    list.Add(obj);
                }
            }

            return list;
        }
    }
}
