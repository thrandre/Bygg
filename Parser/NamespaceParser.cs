using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Bygg.Parser
{
	public class NamespaceParser
	{
		private readonly string _namespaceMatchPattern;

		public NamespaceParser(string namespaceMatchPattern)
		{
			_namespaceMatchPattern = namespaceMatchPattern;
		}

		public string Parse(IEnumerable<string> codeLines)
		{
			foreach (var line in codeLines)
			{
				var match = Regex.Match(line, _namespaceMatchPattern);
				if (match.Success)
				{
					return match.Groups[1].Value;
				}
			}

			return string.Empty;
		}
	}
}