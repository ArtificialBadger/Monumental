using Monument.Types.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monument.Types.Generic
{
    public class OpenGenericComposite<T> : IGeneric<T>
    {
        private readonly IEnumerable<IGeneric<T>> genericNodes;

        public int NodeCount => genericNodes.Count();

        public OpenGenericComposite(IEnumerable<IGeneric<T>> genericNodes)
        {
            this.genericNodes = genericNodes;
        }


    }
}
