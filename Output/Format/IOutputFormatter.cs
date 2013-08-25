using System;
using System.Collections.Generic;

namespace Bygg.Output.Format
{
	public interface IOutputFormatter
	{
		String Format(IList<String> code, bool padded);
	}
}