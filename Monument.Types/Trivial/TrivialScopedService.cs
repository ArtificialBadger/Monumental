using Monument.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monument.Types.Trivial
{
    [Scoped]
    public sealed class TrivialScopedService : ITrivialScopedService
    {
        private int count;

        public string Serve()
        {
            return $"{++count}";
        }
    }
}
