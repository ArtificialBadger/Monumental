using System;
using System.Collections.Generic;
using System.Text;

namespace Monument.Types.ConstratinedGeneric
{
    public interface IConstrainedGeneric<in T1, out T2> 
        where T1 : IEnumerable<T2>, new() 
        where T2 : struct
    {
    }
}
