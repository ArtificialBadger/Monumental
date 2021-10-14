using Monument.Containers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monument.DotNetContainer
{
    public sealed class DotNetRuntimeContainerAdapter : IRuntimeContainer
    {
        private readonly IServiceProvider serviceProvider;

        public DotNetRuntimeContainerAdapter(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public T Resolve<T>() where T : class
        {
            return (T) this.serviceProvider.GetService(typeof(T));
        }
    }
}
