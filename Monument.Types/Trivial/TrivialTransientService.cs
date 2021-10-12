using Monument.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monument.Types.Trivial
{
    [Transient]
    public sealed class TrivialTransientService : ITrivialTransientService
    {
        private int count;

        public string Serve()
        {
            return $"{++count}";
        }
    }
}
