using System;
using System.Collections.Generic;
using System.Text;

namespace Bygg.Output.Format
{
	public class StandardIndentationFormatter : IOutputFormatter
	{
		public IList<string> Format(IList<string> code)
		{
			for (var i = 1; i < (code.Count - 1); i++)
			{
				code[i] = "\t" + code[i];
			}

			return code;
		}
	}
}