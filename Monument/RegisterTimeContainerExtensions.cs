using System;
using System.Collections.Generic;
using System.Linq;

namespace Monument
{
    public static class RegisterTimeContainerExtensions
    {
        public static IRegisterTimeContainer Register<TService, TImplementation>(this IRegisterTimeContainer container)
            where TImplementation : TService
            => container.Register(typeof(TService), typeof(TImplementation), Lifestyle.Transient);

        public static IRegisterTimeContainer Register(this IRegisterTimeContainer container, Type service, Type implementation)
            => container.Register(service, implementation, implementation.GetLifestyle());

        public static IRegisterTimeContainer RegisterDecorator(this IRegisterTimeContainer container, Type service, Type implementation)
            => container.RegisterDecorator(service, implementation, implementation.GetLifestyle());

        public static IRegisterTimeContainer RegisterAll(this IRegisterTimeContainer container, Type service, IEnumerable<Type> implementations)
            => container.RegisterAll(service, implementations.Select(type => (type, type.GetLifestyle())));
    }
}
