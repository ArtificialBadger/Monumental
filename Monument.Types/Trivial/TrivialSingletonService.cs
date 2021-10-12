using Monument.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monument.Types.Trivial
{
    [Singleton]
    public sealed class TrivialSingletonService : ITrivialSingletonService
    {
        private int count;

        public string Serve()
        {
            return $"{++count}";
        }
    }
}
