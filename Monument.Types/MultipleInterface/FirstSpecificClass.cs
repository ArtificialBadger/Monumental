using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Monument.Types.MultipleInterface;
public class FirstSpecificClass : HighLevelBaseClass, ISpecificInterface
//public class FirstSpecificClass : ISpecificInterface
{
	public void SpecificThing()
	{
		Debug.WriteLine("Did a specific thing in the first specific class");
	}
}
