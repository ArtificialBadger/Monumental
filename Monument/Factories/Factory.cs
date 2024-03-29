﻿using Monument.Containers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monument.Factories
{
    public sealed class Factory<T> : IFactory<T> where T : class
    {
        private IRuntimeContainer container;

        public Factory(IRuntimeContainer container)
        {
            this.container = container;
        }

        public T Produce()
        {
            return this.container.Resolve<T>();
        }
    }
}
