using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Monument.Attributes;
using Monument.Containers;
using Monument.Conventions.Settings;
using Monument.Factories;

namespace Monument.Conventions
{
    public class TypePatternRegistrationConvention : IRegistrationConvention
    {
        private readonly IRegisterTimeContainer registerTimeContainer;

        private RegistrationConventionSettings settings;

        public TypePatternRegistrationConvention(IRegisterTimeContainer registerTimeContainer)
        {
            this.registerTimeContainer = registerTimeContainer;
        }

        public IRegisterTimeContainer Register(IEnumerable<Type> types)
        {
            this.settings = RegistrationConventionSettings.DefaultSettings;
            RegisterTypes(types);
            return registerTimeContainer;
        }

        public IRegisterTimeContainer Register(IEnumerable<Type> types, RegistrationConventionSettings registrationConventionSettings)
        {
            this.settings = registrationConventionSettings;
            RegisterTypes(types);
            return registerTimeContainer;
        }


        public TypePatternRegistrationConvention RegisterTypes(IEnumerable<Type> types)
        {
            var excludedAssemblies = new List<Assembly>() { typeof(IEquatable<>).Assembly };

            var implementationTypes = types
                .Where(type => !type.IsInterface)
                .Where(type => !type.IsAbstract)
                .Where(type => type.IsPublic)
                .Where(type => type.IsClass)
                .Where(type => !type.IsNested)
                .Where(type => type.GetInterfaces().Count() == 1 && excludedAssemblies.All(a => type.GetInterfaces().Single().Assembly != a))
                .Where(type => type.GetConstructors().Count() == 1)
                .Where(type => type.BaseType != typeof(Exception))
                .Where(type => !type.CustomAttributes.Any(a => a.AttributeType == typeof(IgnoreAttribute)));

            var interfaceImplementationGroups = implementationTypes
                .GroupBy(type => type.GetInterfaces().Single().ToTypeKey());

            foreach (var interfaceImplementationGroup in interfaceImplementationGroups)
            {
                var implmentationInterface = interfaceImplementationGroup.Key;

                var composites = interfaceImplementationGroup.Where(type => type.IsComposite());
                var decorators = interfaceImplementationGroup.Where(type => type.IsDecorator());

                var standardImplementations = interfaceImplementationGroup.Except(composites).Except(decorators);

                if (composites.Count() > 1)
                {
                    throw new TypePatternRegistrationException("You cannot register more than one composite.");
                }
                if (!composites.Any() && !standardImplementations.Any())
                {
                    throw new TypePatternRegistrationException("You cannot register only decorators.");
                }

                if (composites.Any())
                {
                    registerTimeContainer.Register(implmentationInterface, composites.Single());
                }
                else
                {
                    if (implmentationInterface.IsGenericType)
                    {
                        foreach (var normalSingleGroup in standardImplementations.GroupBy(type => type.GetInterfaces().Single().GetGenericArguments(), new TypeArrayEqualityComparer()))
                        {
                            if (normalSingleGroup.Count() == 1)
                            {
                                var normal = normalSingleGroup.Single();

                                if (!normal.IsOpenGeneric() && !standardImplementations.Any(n => n.IsOpenGeneric()))
                                {
                                    registerTimeContainer.Register(normal.GetInterfaces().Single(), normal);
                                }
                                else if (normal.IsOpenGeneric())
                                {
                                    registerTimeContainer.Register(implmentationInterface, normal);
                                }

                            }
                        }
                    }
                    else
                    {
                        if (standardImplementations.Count() == 1)
                        {
                            registerTimeContainer.Register(implmentationInterface, standardImplementations.Single());
                        }
                    }

                }

                // What is this thing for? The above should handle it?
                if (standardImplementations.Any())
                {
                    registerTimeContainer.RegisterAll(implmentationInterface, standardImplementations);
                }

                foreach (var decorator in decorators)
                {
                    registerTimeContainer.RegisterDecorator(implmentationInterface, decorator);
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
