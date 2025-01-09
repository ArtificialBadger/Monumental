using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Monument.Types.MultipleInterfaceConstructor;
public class FirstSpecificClass : HighLevelBaseClass, ISpecificInterface
{
	public FirstSpecificClass(IService service)
	{ }

	public void SpecificThing()
	{
		Debug.WriteLine("Did a specific thing in the first specific class");
	}
}
