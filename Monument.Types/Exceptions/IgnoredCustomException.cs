using Monument.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monument.Types.Exceptions
{
    [Ignore]
    public class IgnoredCustomException : Exception
    {
        public IgnoredCustomException()
        {
        }

        public IgnoredCustomException(string message)
            : base(message)
        {
        }

        public IgnoredCustomException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
