using System;
using System.Collections.Generic;
using System.Text;

namespace Bygg.Output.Format
{
	public class OutputFormatter : IOutputFormatter
	{
		public String Format(IList<String> code, bool padded)
		{
			var formatted = new StringBuilder();
			for (var i = 0; i < code.Count; i++)
			{
				var line = code[i];
				if (padded && i != 0 && i != (code.Count - 1))
				{
					line = "\t" + line;
				}
				formatted.AppendLine(line);
			}

			return formatted.ToString();
		}
	}
}