using Monument.Types.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monument.Types.Generic
{
    public class ClosedGenericComposite : IGeneric<Animal>
    {
        private readonly IEnumerable<IGeneric<Animal>> genericNodes;

        public int NodeCount => genericNodes.Count();

        public ClosedGenericComposite(IEnumerable<IGeneric<Animal>> genericNodes)
        {
            this.genericNodes = genericNodes;
        }


    }
}
