using System;
using System.Collections.Generic;
using System.Text;

namespace Monument.Types.ConstratinedGeneric
{
    public class ConstrainedGenericImplementation<T> : IConstrainedGeneric<List<T>, T>
        where T : struct
    {
    }
}
