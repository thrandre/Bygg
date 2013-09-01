using System;
using System.Collections.Generic;
using Bygg.Units;

namespace Bygg.Output.Combine
{
	public interface IOutputCombiner
	{
		string Combine(IEnumerable<CodeUnit> units, string ns);
	}
}