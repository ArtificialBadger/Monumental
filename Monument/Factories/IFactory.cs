using System;
using System.Collections.Generic;
using System.Text;

namespace Monument.Factories
{
    public interface IFactory<out T> where T : class
    {
        public T Produce();
    }
}
