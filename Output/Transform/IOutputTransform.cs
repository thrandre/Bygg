using System;
using System.Collections.Generic;

namespace Bygg.Output.Transform
{
	public interface IOutputTransform
	{
		String Transform(IList<String> codeLines, String ns, bool isNsDependency);
	}
}