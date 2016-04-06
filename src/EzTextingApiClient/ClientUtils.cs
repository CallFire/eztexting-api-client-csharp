using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using EzTextingApiClient.Api.Common.Model;
using EzTextingApiClient.Api.Messaging.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using RestSharp.Extensions;

namespace EzTextingApiClient
{
    /// <summary>
    /// Utility class
    /// </summary>
    public static class ClientUtils
    {
        public static readonly IList<KeyValuePair<string, object>> EmptyMap = new List<KeyValuePair<string, object>>(0);

        /// <summary>
        /// Convert ICollection to pretty string
        /// </summary>
        /// <returns>string representation of IEnumerable object</returns>
        /// <param name="collection">any collection.</param>
        internal static string ToPrettyString<T>(this ICollection<T> collection)
        {
            var builder = new StringBuilder();
            foreach (object o in collection)
            {
                if (o is KeyValuePair<string, object>)
                {
                    var pair = (KeyValuePair<string, object>) o;
                    builder.Append(pair.Key).Append(":");
                    if (pair.Value is ICollection)
                    {
                        foreach (var v in pair.Value as ICollection)
                        {
                            builder.Append(v.ToString()).Append(",");
                        }
                        builder.Remove(builder.Length - 1, 1);
                    }
                    else
                    {
                        builder.Append(pair.Value);
                    }
                }
                else
                {
                    builder.Append(o.ToString());
                }
                builder.Append(", ");
            }
            if (collection.Count > 0)
            {
                builder.Remove(builder.Length - 2, 2);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Replaces the first occurence of given string
        /// </summary>
        /// <returns>updated string</returns>
        /// <param name="text">initial string</param>
        /// <param name="search">substring to replace</param>
        /// <param name="replace">replacement string</param>
        internal static string ReplaceFirst(this string text, string search, string replace)
        {
            var pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }

        internal static void PrintParams(List<Parameter> parameters)
        {
            parameters.ForEach(Console.WriteLine);
        }

        public static IList<KeyValuePair<string, object>> AsParams(string name, object value)
        {
            var queryParams = new List<KeyValuePair<string, object>>();
            if (value is ICollection)
            {
                foreach (var o in (ICollection) value)
                {
                    queryParams.Add(new KeyValuePair<string, object>(name + "[]", o?.ToString()));
                }
            }
            else
            {
                queryParams.Add(new KeyValuePair<string, object>(name, value?.ToString()));
            }

            return queryParams;
        }

        public static IList<KeyValuePair<string, object>> AsParams(string name1, object value1, string name2,
            object value2)
        {
            var queryParams = AsParams(name1, value1);
            queryParams.Add(new KeyValuePair<string, object>(name2, value2?.ToString()));
            return queryParams;
        }

        /// Method traverses request object using reflection and build key=value string
        ///
        /// <param name="request">request</param>
        /// <typeparam name="T">type of request</typeparam>
        /// <returns>string representation of request params</returns>
        /// <exception cref="EzTextingClientException">in case IllegalAccessException occurred</exception>
        ///
        public static StringBuilder BuildQueryParams<T>(T request) where T : EzTextingModel
        {
            var paramsString = new StringBuilder();
            if (request == null)
            {
                return paramsString;
            }
            foreach (var pi in request.GetType().GetProperties())
            {
                var attr = GetPropertyAttributes(pi);
                if (attr.ContainsKey(typeof(JsonIgnoreAttribute).Name))
                {
                    continue;
                }
                var value = pi.GetValue(request, null);
                BuildQueryParam(pi, value, paramsString);
            }

            return paramsString;
        }

        private static void BuildQueryParam(PropertyInfo pi, object value, StringBuilder paramsString)
        {
            if (value == null)
                return;
            var paramName = GetParamName(pi);
            if (value is ICollection)
            {
                foreach (var o in (ICollection) value)
                {
                    paramsString.Append(paramName).Append("=").Append(o.ToString().UrlEncode()).Append("&");
                }
                return;
            }
            else if (value is Enum)
            {
                value = EnumMemberAttr(value);
            }
            else if (value is DateTime)
            {
                value = (ToUnixTime((DateTime) value)/1000);
            }
            var paramValue = GetParamValue(pi, value);
            paramsString.Append(paramName).Append("=").Append(paramValue.UrlEncode()).Append("&");
        }

        internal static string Join<T>(this string separator, ICollection<T> s)
        {
            return s == null ? "" : s.ToPrettyString();
        }

        public static long ToUnixTime(DateTime dateTime)
        {
            return (long) (dateTime.ToUniversalTime() - ClientConstants.Epoch).TotalMilliseconds;
        }

        private static object GetParamName(PropertyInfo pi)
        {
            var name = pi.Name;
            CustomAttributeData jsonPropertyAttr = null;
            GetPropertyAttributes(pi).TryGetValue(typeof(JsonPropertyAttribute).Name, out jsonPropertyAttr);
            // first constructor argument is custom property name
            if (jsonPropertyAttr != null && jsonPropertyAttr.ConstructorArguments.Count == 1)
            {
                name = jsonPropertyAttr.ConstructorArguments[0].Value.ToString();
            }
            if (IsCollectionType(pi.PropertyType))
            {
                name = name + "[]";
            }

            return name;
        }

        private static string GetParamValue(PropertyInfo pi, object value)
        {
            var attr = GetPropertyAttributes(pi);
            if (value is bool && attr.ContainsKey(typeof(QueryParamAsNumber).Name))
            {
                return Convert.ToInt32(value).ToString();
            }
            else if (value is SimpleMessage)
            {
                return ((SimpleMessage) value).Message;
            }
            else if (value is bool)
            {
                return value.ToString().ToLower();
            }
            else
            {
                return value.ToString();
            }
        }

        internal static string ToCamelCase(string s)
        {
            if (string.IsNullOrEmpty(s) || !char.IsUpper(s[0]))
            {
                return s;
            }

            var chars = s.ToCharArray();
            for (var i = 0; i < chars.Length; i++)
            {
                var hasNext = (i + 1 < chars.Length);
                if (i > 0 && hasNext && !char.IsUpper(chars[i + 1]))
                {
                    break;
                }
                chars[i] = char.ToLowerInvariant(chars[i]);
            }
            return new string(chars);
        }

        internal static bool IsCollectionType(this Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ICollection<>))
            {
                return true;
            }
            var genericInterfaces = type.GetInterfaces().Where(t => t.IsGenericType);
            var baseDefinitions = genericInterfaces.Select(t => t.GetGenericTypeDefinition());
            return baseDefinitions.Any(t => t == typeof(ICollection<>));
        }

        private static Dictionary<string, CustomAttributeData> GetPropertyAttributes(PropertyInfo property)
        {
            var attribs = new Dictionary<string, CustomAttributeData>();
            foreach (var attribData in property.GetCustomAttributesData())
            {
                var typeName = attribData.Constructor.DeclaringType?.Name;
                attribs[typeName] = attribData;
            }
            return attribs;
        }

        /// <summary>
        /// Returns a EnumMember attribute for object
        /// </summary>
        /// <param name="source">object to select EnumMember attr from</param>
        /// <returns>EnumMemberAttribute for input object</returns>
        internal static string EnumMemberAttr<T>(this T source)
        {
            var fi = source.GetType().GetField(source.ToString());
            var attributes = (EnumMemberAttribute[]) fi.GetCustomAttributes(typeof(EnumMemberAttribute), false);

            return attributes.Length > 0 ? attributes[0].Value : source.ToString();
        }

        /// <summary>
        /// Returns a EnumMember attribute for object
        /// </summary>
        /// <param name="source">object to select EnumMember attr from</param>
        /// <returns>EnumMemberAttribute for input object</returns>
        internal static string DescriptionAttr<T>(this T source)
        {
            var fi = source.GetType().GetField(source.ToString());
            var attributes = (DescriptionAttribute[]) fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : source.ToString();
        }

        /// <summary>
        /// Returns enum by attribute value
        /// </summary>
        /// <param name="value">enum item string representation</param>
        /// <returns>enum object</returns>
        internal static T EnumFromString<T>(string value)
        {
            var type = typeof(T);
            if (!type.IsEnum)
            {
                throw new ArgumentException("type T is not enum type");
            }
            var fields = type.GetFields();
            var field = fields.SelectMany(f => f.GetCustomAttributes(typeof(EnumMemberAttribute), false),
                (f, a) => new {Field = f, Att = a}).SingleOrDefault(a => ((EnumMemberAttribute) a.Att).Value == value);

            return field == null ? default(T) : (T) field.Field.GetRawConstantValue();
        }

        /// <summary>
        /// Returns int converted from string with default possible value
        /// </summary>
        /// <param name="s">string to convert</param>
        /// <param name="def">default to return if s is null</param>
        /// <returns>int value</returns>
        internal static int StrToIntDef(string s, int def)
        {
            return !int.TryParse(s, out def) ? def : int.Parse(s);
        }
    }
}