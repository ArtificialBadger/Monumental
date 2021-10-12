using Microsoft.Extensions.DependencyInjection;
using Monument.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using M = Monument;

namespace Monument.DotNetContainer
{
    public sealed class DotNetContainerAdapter : IRegisterTimeContainer
    {
        private readonly IServiceCollection serviceCollection;

        public DotNetContainerAdapter(IServiceCollection serviceCollection)
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
            //throw new Exception(""); //TODO ArtificialBadger : Scrutor Dependency? Handling this internally seems pretty hard to be honest
            //serviceCollection.RegisterDecorator(service, implementation, Convert(lifestyle));
            RegisterInternal(service, implementation, lifestyle);
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

        private IServiceCollection RegisterInternal(Type service, IEnumerable<Type> implementations, Lifestyle lifestyle)
        {
            foreach (var implementation in implementations)
            {
                this.RegisterInternal(service, implementation, lifestyle);
            }

            return this.serviceCollection;
        }
    }
}
