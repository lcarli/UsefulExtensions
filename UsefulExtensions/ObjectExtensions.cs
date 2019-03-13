using System;
using System.ComponentModel;
using System.Linq;

namespace UsefulExtensions
{
    public static class ObjectExtensions
    {
        public static TTarget? To<TTarget>(this object instance) where TTarget : struct
        {
            var stringValue = instance?.ToString().ToLower();
            return stringValue?.To<TTarget>();
        }

        public static TTarget To<TTarget>(this object instance, TTarget defaultValue)
           where TTarget : struct
        {
            return instance?.To<TTarget>() ?? defaultValue;
        }

        public static string GetDescription(this object item)
        {
            var memInfo = item.GetType().GetMember(item.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            var description = ((DescriptionAttribute)attributes[0]).Description;
            return description;
        }

        public static void SetProperty(this object instance, string propertyName, object value)
        {
            var property = instance.GetType().GetProperty(propertyName);

            if (property != null)
            {
                property.SetValue(instance, value, null);
            }
        }

        public static T GetProperty<T>(this object instance, string propertyName)
        {
            var property = instance.GetType().GetProperty(propertyName);
            if (property != null)
            {
                return (T)property.GetValue(instance, null);
            }
            else
            {
                return default(T);
            }
        }

        public static T CallMethod<T>(this object instance, string methodName, params object[] parameters)
        {
            var method = instance.GetType().GetMethod(methodName, parameters.Select(parameter => parameter.GetType()).ToArray());

            if (method != null)
            {
                return (T)method.Invoke(instance, parameters);
            }

            return default(T);
        }

        public static void CallVoidMethod<T>(this object instance, string methodName, params object[] parameters)
        {
            var method = instance.GetType().GetMethod(methodName, parameters.Select(parameter => parameter.GetType()).ToArray());

            if (method != null)
            {
                method.Invoke(instance, parameters);
            }
        }

        public static void CallGenericVoidMethod(this object instance, Type argumentType, string methodName, params object[] parameters)
        {
            var method = instance.GetType().GetMethod(methodName, parameters.Select(parameter => parameter.GetType()).ToArray());
            if (method != null)
            {
                method.MakeGenericMethod(argumentType).Invoke(instance, parameters);
            }
        }

        public static T CallGenericMethod<T>(this object instance, Type argumentType, string methodName,
           params object[] parameters)
        {
            var method = instance.GetType().GetMethod(methodName, parameters.Select(parameter => parameter.GetType()).ToArray());
            if (method != null)
            {
                return (T)method.MakeGenericMethod(argumentType).Invoke(instance, parameters);
            }

            return default(T);
        }
    }
}
