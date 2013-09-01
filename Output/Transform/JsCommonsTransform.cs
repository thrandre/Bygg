using System;
using System.Collections.Generic;
using Bygg.Output.Format;

namespace Bygg.Output.Transform
{
	public class JsCommonsTransform : IOutputTransform
	{
		public IList<string> Transform(IList<string> codeLines, string ns)
		{
			codeLines.Insert(0, string.Format(@"(function({0}) {{", ns));
			codeLines.Add(string.Format(@"}})({0} || ({0} = {{}}));", ns));

			return codeLines;
		}
	}
}