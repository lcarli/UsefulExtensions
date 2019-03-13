using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace UsefulExtensions
{
    public static class EnumExtensions
    {
        public static NameValueCollection ToList<T>() where T : struct
        {
            var result = new NameValueCollection();
            if (!typeof(T).IsEnum) return result;
            var enumType = typeof(T);
            var values = Enum.GetValues(enumType);
            foreach (var value in values)
            {
                var description = value.GetDescription() ?? value.ToString();
                result.Add(description, value.ToString());
            }
            return result;
        }

        public static List<KeyValuePair<int, string>> ToKeyValuePairs<T>(this Enum instance)
        {
            return Enum.GetValues(typeof(T)).Cast<T>().Select(item =>
                new KeyValuePair<int, string>(Convert.ToInt32(item), item.GetDescription())).OrderBy(item => item.Key).ToList();
        }

        public static List<T> ToList<T>(this Enum type)
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToList();
        }

        public static bool Has<T>(this Enum type, T value)
        {
            try
            {
                return ((int)(object)type & (int)(object)value) == (int)(object)value;
            }
            catch
            {
                return false;
            }
        }

        public static bool Is<T>(this Enum type, T value)
        {
            try
            {
                return (int)(object)type == (int)(object)value;
            }
            catch
            {
                return false;
            }
        }

        public static T Add<T>(this Enum type, T value)
        {
            try
            {
                return (T)(object)(((int)(object)type | (int)(object)value));
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Could not append value from enumerated type '{typeof(T).Name}'.", ex);
            }
        }

        public static T Remove<T>(this Enum type, T value)
        {
            try
            {
                return (T)(object)(((int)(object)type & ~(int)(object)value));
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Could not remove value from enumerated type '{typeof(T).Name}'.", ex);
            }
        }

      

        public static string ToCommaSeparatedIntegerList(this Enum type)
        {
            return string.Join(",", type.ToList<int>().ConvertAll(i => i.ToString()).ToArray());
        }
    }
}
