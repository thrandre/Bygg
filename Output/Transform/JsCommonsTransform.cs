using System;
using System.Collections.Generic;
using Bygg.Output.Format;

namespace Bygg.Output.Transform
{
	public class JsCommonsTransform : IOutputTransform
	{
		private readonly IOutputFormatter _formatter;

		public JsCommonsTransform(IOutputFormatter formatter)
		{
			_formatter = formatter;
		}

		public String Transform(IList<String> codeLines, String ns, bool isNsDependency)
		{
			if (!isNsDependency)
			{
				codeLines.Insert(0, String.Format(@"(function({0}) {{", ns));
				codeLines.Add(String.Format(@"}})({0} || ({0} = {{}}));", ns));
			}

			return _formatter.Format(codeLines, !isNsDependency);
		}
	}
}