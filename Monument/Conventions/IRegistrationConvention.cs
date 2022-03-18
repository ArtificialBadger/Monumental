using Monument.Containers;
using Monument.Conventions.Settings;
using System;
using System.Collections.Generic;

namespace Monument.Conventions
{
    public interface IRegistrationConvention
    {
        IRegisterTimeContainer Register(IEnumerable<Type> types);

        IRegisterTimeContainer Register(IEnumerable<Type> types, RegistrationConventionSettings registrationConventionSettings);
    }
}