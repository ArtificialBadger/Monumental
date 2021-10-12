using System;
using System.Collections.Generic;
using System.Text;

namespace Monument.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class SingletonAttribute : LifestyleAttribute
    {
    }
}
