using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Monument.Types.MultipleInterface;
public class SecondSpecificClass : HighLevelBaseClass, ISecondSpecificInterface
//public class SecondSpecificClass : ISecondSpecificInterface
{
	public void SpecificThing()
	{
		Debug.WriteLine("Did a specific thing in the second specific class");
	}
}
