using Microsoft.Extensions.DependencyInjection;
using Monument.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using M = Monument;
using Scrutor;

namespace Monument.DotNetContainer
{
    public sealed class DotNetRegisterTimeContainerAdapter : IRegisterTimeContainer
    {
        private readonly IServiceCollection serviceCollection;

        public DotNetRegisterTimeContainerAdapter(IServiceCollection serviceCollection)
        {
            this.serviceCollection = serviceCollection;
        }

        public IRegisterTimeContainer Register(Type service, Type implementation, Lifestyle lifestyle)
        {
            RegisterInternal(service, implementation, lifestyle);
            return this;
        }

        public IRegisterTimeContainer RegisterDecorator(Type service, Type implementation, Lifestyle lifestyle)
        {
            // Depending on the Scrutor Nuget for now, but a better solution is to rip the relevant code out of Scrutor instead
            serviceCollection.Decorate(service, implementation);
            return this;
        }

        public IRegisterTimeContainer RegisterAll(Type service, IEnumerable<(Type type, Lifestyle lifestyle)> implementations)
        {
            foreach (var implementationsByLifestyle in implementations.GroupBy(imp => imp.lifestyle))
            {
                RegisterInternal(
                    service,
                    implementationsByLifestyle.Select(impl => impl.type),
                    implementationsByLifestyle.Key
                    );
            }

            return this;
        }

        private IServiceCollection RegisterInternal(Type service, Type implementation, Lifestyle lifestyle)
        {
            return lifestyle switch
            {
                Lifestyle.Transient => this.serviceCollection.AddTransient(service, implementation),
                Lifestyle.Scoped => this.serviceCollection.AddScoped(service, implementation),
                Lifestyle.Singleton => this.serviceCollection.AddSingleton(service, implementation),
                _ => throw new Exception("No known lifestyle established"), //TODO ArtificialBadger : Custom Exception Type
            };
        }

        private IServiceCollection RegisterInternal(Type service, Func<object> implementation, Lifestyle lifestyle)
        {
            return lifestyle switch
            {
                Lifestyle.Transient => this.serviceCollection.AddTransient(service, _ => implementation()),
                Lifestyle.Scoped => this.serviceCollection.AddScoped(service, _ => implementation()),
                Lifestyle.Singleton => this.serviceCollection.AddSingleton(service, _ => implementation()),
                _ => throw new Exception("No known lifestyle established"), //TODO ArtificialBadger : Custom Exception Type
            };
        }

        private IServiceCollection RegisterInternal(Type service, IEnumerable<Type> implementations, Lifestyle lifestyle)
        {
            foreach (var implementation in implementations)
            {
                this.RegisterInternal(service, implementation, lifestyle);
            }

            return this.serviceCollection;
        }

        public IRegisterTimeContainer Register(Type service, Func<object> implementationFactory, Lifestyle lifestyle)
        {
            this.RegisterInternal(service, implementationFactory, lifestyle);

            return this;
        }
    }
}
