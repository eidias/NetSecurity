using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Core.Helpers
{
    public static class ReflectionHelper
    {
        public static IEnumerable<FieldInfo> GetConstantsFromType(Type type)
        {
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (var field in fields)
            {
                yield return field;
            }
        }

        /// <summary>
        /// Gets the element of a supplied array or generic collection.
        /// </summary>
        public static Type GetItemType(object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            var type = value.GetType();
            if (type.IsArray)
            {
                return type.GetElementType();
            }

            var genericArgument = type.GetGenericArguments().FirstOrDefault();
            if (genericArgument != null)
            {
                return genericArgument;
            }

            throw new ArgumentException("Method can only be used on arrays or generic collections.");
        }
    }
}
