using System;
using System.Collections.Generic;
using System.Text;

namespace Monument.Containers
{
    public interface IConvertableContainer : IRegisterTimeContainer
    {
        IRuntimeContainer ToRuntimeContainer();
    }
}
