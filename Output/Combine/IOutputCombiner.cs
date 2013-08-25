using System;
using System.Collections.Generic;
using Bygg.Units;

namespace Bygg.Output.Combine
{
	public interface IOutputCombiner
	{
		String Combine(IList<CodeUnit> units, String ns);
	}
}