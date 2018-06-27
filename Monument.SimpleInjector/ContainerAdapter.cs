using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using M = Monument;
using SI = SimpleInjector;

namespace Monument.SimpleInjector
{
    public sealed class ContainerAdapter : IRegisterTimeContainer, IRuntimeContainer
    {
        private readonly Container container;

        public ContainerAdapter(Container container)
        {
            this.container = container;
        }

        public IRegisterTimeContainer Register(Type service, Type implementation, Lifestyle lifestyle)
        {
            container.Register(service, implementation, Convert(lifestyle));
            return this;
        }
        
        public IRegisterTimeContainer RegisterDecorator(Type service, Type implementation, Lifestyle lifestyle)
        {
            container.RegisterDecorator(service, implementation, Convert(lifestyle));
            return this;
        }

        public IRuntimeContainer ToRuntimeContainer()
        {
            container.Verify();
            return this;
        }

        private static SI.Lifestyle Convert(M.Lifestyle lifestyle)
        {
            switch (lifestyle)
            {
                case Lifestyle.Singleton:
                    return SI.Lifestyle.Singleton;
                case Lifestyle.Scoped:
                    return SI.Lifestyle.Scoped;
                case Lifestyle.Transient:
                default:
                    return SI.Lifestyle.Transient;
            }
        }

        public IRegisterTimeContainer RegisterAll(Type service, IEnumerable<(Type type, Lifestyle lifestyle)> implementations)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            container.RegisterCollection(service, implementations.Select(impl => Convert(impl.lifestyle).CreateRegistration(impl.type, container)));
#pragma warning restore CS0618 // Type or member is obsolete
            return this;
        }

        public T Resolve<T>() where T: class => container.GetInstance<T>();
    }
}
