using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Monument.Types.MultipleInterfaceConstructor;
public class SecondSpecificClass : HighLevelBaseClass, ISecondSpecificInterface
{
	public SecondSpecificClass(IService service) { }

	public void SpecificThing()
	{
		Debug.WriteLine("Did a specific thing in the second specific class");
	}
}
