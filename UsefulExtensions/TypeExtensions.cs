using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace UsefulExtensions
{
    public static class TypeExtensions
    {
        public static Type Of(this Type instance, params Type[] argumentTypes)
        {
            if (!IsGeneric(instance)) return null;

            var genericType = instance.MakeGenericType(argumentTypes);
            return genericType;
        }

        public static object New(this Type instance, params object[] parameters)
        {
            var typedtypeInstance = Activator.CreateInstance(instance, parameters);
            return typedtypeInstance;
        }

        public static bool IsGeneric(this Type instance)
        {
            if (instance.IsGenericType)
                return true;

            var baseType = instance.BaseType;
            while (baseType != null)
            {
                if (baseType.IsGenericType)
                    return true;

                baseType = baseType.BaseType;
            }

            return false;
        }

        public static bool Is(this Type instance, string typeName)
        {
            if (instance.Name == typeName)
                return true;

            var baseType = instance.BaseType;
            while (baseType != null)
            {
                if (baseType.Name == typeName)
                    return true;

                baseType = baseType.BaseType;
            }

            return false;
        }

        public static IEnumerable<KeyValuePair<Type, MethodInfo>> GetExtensionMethods(this Type instance)
        {
            if (!instance.IsSealed ||
                instance.IsGenericType ||
                instance.IsNested)
            {
                return Enumerable.Empty<KeyValuePair<Type, MethodInfo>>();
            }

            var methods = instance.GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(m => m.IsDefined(typeof(ExtensionAttribute), false));

            var pairs = new List<KeyValuePair<Type, MethodInfo>>();
            foreach (var method in methods)
            {
                var parameters = method.GetParameters();

                if (parameters.Length <= 0) continue;

                if (parameters[0].ParameterType.IsGenericParameter)
                {
                    if (!method.ContainsGenericParameters) continue;

                    var genericParameters = method.GetGenericArguments();
                    var genericParam = genericParameters[parameters[0].ParameterType.GenericParameterPosition];

                    pairs.AddRange(genericParam.GetGenericParameterConstraints().Select(constraint =>
                        new KeyValuePair<Type, MethodInfo>(parameters[0].ParameterType, method)));
                }
                else
                    pairs.Add(new KeyValuePair<Type, MethodInfo>(parameters[0].ParameterType, method));
            }

            return pairs;
        }
    }
}
