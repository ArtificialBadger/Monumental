using Monument.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;

namespace Monument
{
    public static class TypeExtensions
    {
        public static Type ToTypeKey(this Type type) => type.IsGenericType ? type.GetGenericTypeDefinition() : type;

        public static Type ToOpenGenericTypeKey(this Type type) => type.IsOpenGeneric() ? type.GetGenericTypeDefinition() : type;

        public static IEnumerable<Type> GetRegisterableInterfaces(this Type type, ISet<Assembly> excludedAssemblies = null)
        {
            var baseClassInterfaces = type.BaseType?.GetInterfaces() ?? new Type[] { };
            var types = type.GetInterfaces().Except(baseClassInterfaces);

            if (excludedAssemblies != null)
                return types.Where(t => !excludedAssemblies.Contains(t.Assembly));
            else
                return types;
        }

        public static bool ImplementsInterface(this Type type, Type parentType) => parentType.IsInterface
            && (parentType.IsAssignableFrom(type)
            || (parentType.IsGenericType
            && type.GetInterfaces().Select(ToTypeKey).Contains(parentType.ToTypeKey())));

        public static bool IsOpenGeneric(this Type type) => type.ContainsGenericParameters;

        public static bool HasInterfaces(this Type type) => type.GetInterfaces().Any();

        public static bool IsComposite(this Type type) => type.HasInterfaces()
            && type.GetConstructors().Single().GetParameters()
                .Where(p => p.ParameterType.IsGenericType)
                .Where(p => p.ParameterType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                .Any(p => p.ParameterType.GetGenericArguments().Single().ToOpenGenericTypeKey() == type.GetRegisterableInterfaces().First().ToOpenGenericTypeKey());

        public static bool IsDecorator(this Type type) => type.HasInterfaces()
            && type.GetConstructors().Single().GetParameters()
                .Any(p => p.ParameterType.ToOpenGenericTypeKey() == type.GetRegisterableInterfaces().First().ToOpenGenericTypeKey());

        public static Lifestyle GetLifestyle(this Type type)
        {
            var lifestyle = Lifestyle.Transient;

            if (type.CustomAttributes.Count() > 0)
            {
                var lifestyleAttributes = type.CustomAttributes.Where(a => a.AttributeType.IsSubclassOf(typeof(LifestyleAttribute)));

                if (lifestyleAttributes.Count() > 1)
                {
                    throw new Exception("Too Many Lifestyles"); //TODO ArtificialBadger: custom exception type
                }
                else if (lifestyleAttributes.Any())
                {
                    var attribute = lifestyleAttributes.First().AttributeType;
                    
                    if (attribute.IsAssignableFrom(typeof(SingletonAttribute)))
                    {
                        lifestyle = Lifestyle.Singleton;
                    }
                    else if (attribute.IsAssignableFrom(typeof(TransientAttribute)))
                    {
                        lifestyle = Lifestyle.Transient;
                    }
                    else if (attribute.IsAssignableFrom(typeof(ScopedAttribute)))
                    {
                        lifestyle = Lifestyle.Scoped;
                    }
                }
            }

            return lifestyle;
        }
    }
}
