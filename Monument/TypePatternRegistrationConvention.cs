using System;
using System.Collections.Generic;
using System.Linq;

namespace Monument
{
    public class TypePatternRegistrationConvention : IRegistrationConvention
    {
        public IRegisterTimeContainer Register(IEnumerable<Type> types, IRegisterTimeContainer container)
        {
            var implementationTypes = types
                .Where(type => !type.IsInterface)
                .Where(type => !type.IsAbstract)
                .Where(type => type.IsPublic)
                .Where(type => !type.IsNested)
                .Where(type => type.GetInterfaces().Count() == 1)
                .Where(type => type.GetConstructors().Count() == 1);

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
                    container.Register(inter, composites.Single());
                }
                else
                {
                    if (inter.IsGenericType) {
                        foreach (var normalSingleGroup in normals.GroupBy(type => type.GetInterfaces().Single().GetGenericArguments())) {
                            if (normalSingleGroup.Count() == 1) {
                                var normal = normalSingleGroup.Single();

                                if (!normal.IsOpenGeneric() && !normals.Any(n => n.IsOpenGeneric())) {
                                    container.Register(normal.GetInterfaces().Single(), normal);
                                } else if (normal.IsOpenGeneric()) {
                                    container.Register(inter, normal);
                                }

                            }
                        }
                    } else {
                        if (normals.Count() == 1) {
                            container.Register(inter, normals.Single());
                        }
                    }

                }

                if (normals.Any())
                {
                    container.RegisterAll(inter, normals);
                }
                
                foreach (var decorator in decorators)
                {
                    container.RegisterDecorator(inter, decorator);
                }
            }

            return container;
        }
    }
}
