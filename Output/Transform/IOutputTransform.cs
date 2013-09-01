using System;
using System.Collections.Generic;

namespace Bygg.Output.Transform
{
	public interface IOutputTransform
	{
		IList<string> Transform(IList<string> codeLines, string ns);
	}
}