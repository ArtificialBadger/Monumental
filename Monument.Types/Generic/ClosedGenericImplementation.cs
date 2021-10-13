using System;
using System.Collections.Generic;
using System.Text;

namespace Monument.Types
{
    public class ClosedGenericImplementation1 : IGenericInterface<SimpleImplementation1> { }

    public class ClosedGenericImplementation2 : IGenericInterface<SimpleImplementation1> { }

    public class ClosedGenericImplementation3 : IGenericInterface<SimpleImplementation1> { }

    public class ClosedGenericImplementation4 : IGenericInterface<SimpleImplementation2> { }
}
