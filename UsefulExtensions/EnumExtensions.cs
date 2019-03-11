using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;

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
                var memInfo = enumType.GetMember(enumType.GetEnumName(value));
                var descriptionAttributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                var description = descriptionAttributes.Length > 0
                    ? ((DescriptionAttribute)descriptionAttributes.First()).Description
                    : value.ToString();
                result.Add(description, value.ToString());
            }
            return result;
        }
    }
}
