using System;
using System.Collections.Generic;
using System.Text;

namespace Monument.Types.Trivial
{
    public sealed class TrivialService : ITrivialService
    {
        public string Serve()
        {
            return "What did you expect?";
        }
    }
}
