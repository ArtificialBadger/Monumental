// Ripped from https://github.com/khellang/Scrutor (MIT Liscense)
// Will massage this into a more usable format at some point


//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Monument.DotNetContainer.ScrutorPulls
//{
//    internal static class Extentions
//    {
//        public static bool TryDecorate(this IServiceCollection services, DecorationStrategy strategy)
//        {
//            var decorated = false;

//            for (var i = services.Count - 1; i >= 0; i--)
//            {
//                var serviceDescriptor = services[i];

//                if (serviceDescriptor.ServiceType is DecoratedType)
//                {
//                    continue; // Service has already been decorated.
//                }

//                if (!strategy.CanDecorate(serviceDescriptor.ServiceType))
//                {
//                    continue; // Unable to decorate using the specified strategy.
//                }

//                var decoratedType = new DecoratedType(serviceDescriptor.ServiceType);

//                // Insert decorated
//                services.Add(serviceDescriptor.WithServiceType(decoratedType));

//                // Replace decorator
//                services[i] = serviceDescriptor.WithImplementationFactory(strategy.CreateDecorator(decoratedType));

//                decorated = true;
//            }

//            return decorated;
//        }

//        public static ServiceDescriptor WithImplementationFactory(this ServiceDescriptor descriptor, Func<IServiceProvider, object> implementationFactory)
//        {
//            return new ServiceDescriptor(descriptor.ServiceType, implementationFactory, descriptor.Lifetime);
//        }

//        public static ServiceDescriptor WithServiceType(this ServiceDescriptor descriptor, Type serviceType)
//        {
//            if (descriptor.ImplementationType != null)
//            {
//                return new ServiceDescriptor(serviceType, descriptor.ImplementationType, descriptor.Lifetime);
//            }
//            else if (descriptor.ImplementationFactory != null)
//            {
//                return new ServiceDescriptor(serviceType, descriptor.ImplementationFactory, descriptor.Lifetime);
//            }
//            else if (descriptor.ImplementationInstance != null)
//            {
//                return new ServiceDescriptor(serviceType, descriptor.ImplementationInstance);
//            }
//            else
//            { 
//                throw new ArgumentException($"No implementation factory or instance or type found for {descriptor.ServiceType}.", nameof(descriptor))
//            }
//        };

//    }
//}
