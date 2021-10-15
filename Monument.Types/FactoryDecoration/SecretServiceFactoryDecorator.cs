using Monument.Attributes;
using Monument.Factories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monument.Types.FactoryDecoration
{
    [Ignore] // SimpleInjector does not support this, it only supports Func<T>, not IFactory<T>
    public sealed class SecretServiceFactoryDecorator : ISecretService
    {
        private readonly Func<ISecretService> factory;

        public SecretServiceFactoryDecorator(Func<ISecretService> factory)
        {
            this.factory = factory;
        }
    }
}
