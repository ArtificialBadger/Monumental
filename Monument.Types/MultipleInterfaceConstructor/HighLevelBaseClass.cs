using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Monument.Types.MultipleInterfaceConstructor;
public abstract class HighLevelBaseClass : IHighLevelInterface
{
	public void HighLevelThing()
	{
		Debug.WriteLine("Did a high level thing in the base class");
	}
}
