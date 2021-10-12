using Monument.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monument.Types.Trivial
{
    [Singleton]
    public sealed class TrivialService : ITrivialService
    {
        private int count;

        public string Serve()
        {
            return $"{++count}";
        }
    }
}
