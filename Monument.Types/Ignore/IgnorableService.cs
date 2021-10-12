using Monument.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monument.Types.Ignore
{
    [Ignore]
    public class IgnorableService : IIgnorableService
    {
        public IgnorableService()
        {
            throw new Exception("This constructor should never be hit"); //TODO ArtificialBadger: Custom Exception Type
        }
    }
}
