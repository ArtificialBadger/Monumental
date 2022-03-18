using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Monument.Attributes;
using Monument.Containers;
using Monument.Conventions.Settings;
using Monument.Factories;

namespace Monument.Conventions
{
    public class TypePatternRegistrationConvention : IRegistrationConvention
    {
        private readonly IRegisterTimeContainer registerTimeContainer;

        private readonly RegistrationConventionSettings settings;

        public TypePatternRegistrationConvention(IRegisterTimeContainer registerTimeContainer)
        {
            this.registerTimeContainer = registerTimeContainer;
        }

        public IRegisterTimeContainer Register(IEnumerable<Type> types)
        {
            RegisterTypes(types);
            return registerTimeContainer;
        }

        public TypePatternRegistrationConvention RegisterTypes(IEnumerable<Type> types)
        {
            var implementationTypes = types
                .Where(type => !type.IsInterface)
                .Where(type => !type.IsAbstract)
                .Where(type => type.IsPublic)
                .Where(type => !type.IsNested)
                .Where(type => type.GetInterfaces().Count() == 1)
                .Where(type => type.GetConstructors().Count() == 1)
                .Where(type => !type.CustomAttributes.Any(a => a.AttributeType == typeof(IgnoreAttribute)));

            var byInterface = implementationTypes
                .GroupBy(type => type.GetInterfaces().Single().ToTypeKey());

            foreach (var implementations in byInterface)
            {
                var inter = implementations.Key;

                var composites = implementations.Where(type => type.IsComposite());
                var decorators = implementations.Where(type => type.IsDecorator());

                var normals = implementations.Except(composites).Except(decorators);

                if (composites.Count() > 1)
                {
                    throw new TypePatternRegistrationException("You cannot register more than one composite.");
                }
                if (!composites.Any() && !normals.Any())
                {
                    throw new TypePatternRegistrationException("You cannot register only decorators.");
                }

                if (composites.Any())
                {
                    registerTimeContainer.Register(inter, composites.Single());
                }
                else
                {
                    if (inter.IsGenericType)
                    {
                        foreach (var normalSingleGroup in normals.GroupBy(type => type.GetInterfaces().Single().GetGenericArguments(), new TypeArrayEqualityComparer()))
                        {
                            if (normalSingleGroup.Count() == 1)
                            {
                                var normal = normalSingleGroup.Single();

                                if (!normal.IsOpenGeneric() && !normals.Any(n => n.IsOpenGeneric()))
                                {
                                    registerTimeContainer.Register(normal.GetInterfaces().Single(), normal);
                                }
                                else if (normal.IsOpenGeneric())
                                {
                                    registerTimeContainer.Register(inter, normal);
                                }

                            }
                        }
                    }
                    else
                    {
                        if (normals.Count() == 1)
                        {
                            registerTimeContainer.Register(inter, normals.Single());
                        }
                    }

                }

                // What is this thing for? The above should handle it?
                if (normals.Any())
                {
                    registerTimeContainer.RegisterAll(inter, normals);
                }

                foreach (var decorator in decorators)
                {
                    registerTimeContainer.RegisterDecorator(inter, decorator);
                }
            }

            return this;
        }

        public TypePatternRegistrationConvention RegisterFactory(IRuntimeContainer runtimeContainer)
        {
            registerTimeContainer.Register(typeof(IRuntimeContainer), () => runtimeContainer, Lifestyle.Singleton);
            registerTimeContainer.Register(typeof(IFactory<>), typeof(Factory<>));
            return this;
        }

        public IRegisterTimeContainer UseSettings(RegistrationConventionSettings registrationConventionSettings)
        {
            throw new NotImplementedException(); // Because I am bad at what I do
        }

        private class TypeArrayEqualityComparer : IEqualityComparer<Type[]>
        {
            public bool Equals(Type[] x, Type[] y)
            {
                return x.SequenceEqual(y);
            }

            public int GetHashCode(Type[] obj)
            {
                unchecked
                {
                    int hash = 17;

                    foreach (var item in obj)
                    {
                        hash = hash * 23 + item.GetHashCode();
                    }

                    return hash;
                }
            }
        }
    }
}
