using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Bygg.Dependencies;

namespace Bygg.Parser
{
	public class DependencyParser
	{
		private const string UrlMatchPattern = @"http[s]?\://";
		private const string FileMatchPattern = @"\.js$";

		private readonly string _dependencyMatchPattern;
		
		public DependencyParser(string dependencyMatchPattern)
		{
			_dependencyMatchPattern = dependencyMatchPattern;
		}

		public IList<Dependency> Parse(IEnumerable<string> codeLines)
		{
			var dependencies = new List<Dependency>();
			
			foreach (var codeLine in codeLines)
			{
				var match = Regex.Match(codeLine, _dependencyMatchPattern);
				
				if (!match.Success)
				{
					continue;
				}

				var path = match.Groups[1].Value;
				var isNamespaceDependency = false;

				if (match.Groups[3].Success)
				{
					isNamespaceDependency = Boolean.Parse(match.Groups[4].Value);
				}

				dependencies.Add
					(
						new FileDependency(path, isNamespaceDependency)
					);
			}

			return dependencies;
		}
	}
}