using System;
using System.Collections.Generic;

namespace Monument.Containers
{
    public interface IRegisterTimeContainer
    {
        IRegisterTimeContainer Register(Type service, Type implementation, Lifestyle lifestyle);
        IRegisterTimeContainer RegisterAll(Type service, IEnumerable<(Type type, Lifestyle lifestyle)> implementations);
        IRegisterTimeContainer RegisterDecorator(Type service, Type implementation, Lifestyle lifestyle);
    }
}