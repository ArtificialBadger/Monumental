using System;
using System.Collections.Generic;
using System.Text;

namespace Monument.Conventions.Settings
{
    // I hate not giving this an abstraction, but I just don't think it's going to be necessary since this is essentially just a DTO
    public sealed class RegistrationConventionSettings
    {
        public AbstractClassRegistrationPolicy AbstractClassRegistrationPolicy { get; set; }

        public static readonly RegistrationConventionSettings DefaultSettings = new RegistrationConventionSettings();
    }
}
