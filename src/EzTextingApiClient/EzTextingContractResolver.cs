using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EzTextingApiClient
{
    /// <summary>
    /// Custom ContactResolver is used for:
    ///   1. Populate properties with private modifiers.
    ///   2. Skip serialization of empty collections.
    ///   3. Base class properties go first on serialization.
    /// </summary>
    public class EzTextingContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var prop = base.CreateProperty(member, memberSerialization);
            var pi = member as PropertyInfo;
            if (pi != null)
            {
                prop.Readable = (pi.GetGetMethod(true) != null);
                prop.Writable = (pi.GetSetMethod(true) != null);
            }
            var shouldSerialize = prop.ShouldSerialize;
            prop.ShouldSerialize = obj => (shouldSerialize == null || shouldSerialize(obj)) && !IsEmptyCollection(prop, obj);
            return prop;
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var properties = base.CreateProperties(type, memberSerialization);
            return properties?.OrderBy(p => p.DeclaringType.BaseTypesAndSelf().Count()).ToList() ?? properties;
        }

        private static bool IsEmptyCollection(JsonProperty property, object target)
        {
            var value = property.ValueProvider.GetValue(target);
            var collection = value as ICollection;
            if (collection != null && collection.Count == 0)
            {
                return true;
            }
            if (!typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
            {
                return false;
            }
            var countProp = property.PropertyType.GetProperty("Count");
            if (countProp == null)
            {
                return false;
            }

            var count = (int)countProp.GetValue(value, null);
            return count == 0;
        }
    }

    internal static class TypeExtensions
    {
        public static IEnumerable<Type> BaseTypesAndSelf(this Type type)
        {
            while (type != null)
            {
                yield return type;
                type = type.BaseType;
            }
        }
    }
}

