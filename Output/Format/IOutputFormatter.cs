using System;
using System.Collections.Generic;

namespace Bygg.Output.Format
{
	public interface IOutputFormatter
	{
		IList<string> Format(IList<string> code);
	}
}