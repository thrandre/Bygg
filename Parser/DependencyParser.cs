using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Bygg.Dependencies;

namespace Bygg.Parser
{
	public class DependencyParser
	{
		private const String UrlMatchPattern = @"http[s]?\://";
		
		private readonly String _dependencyMatchPattern;
		
		public DependencyParser(String dependencyMatchPattern)
		{
			_dependencyMatchPattern = dependencyMatchPattern;
		}

		public IList<Dependency> Parse(IList<String> codeLines)
		{
			var dependencies = new List<Dependency>();
			
			foreach (var codeLine in codeLines)
			{
				var match = Regex.Match(codeLine, _dependencyMatchPattern);
				
				if (!match.Success) continue;

				var isNsDependency = match.Groups[1].Value == "ns";
				var dependencyPath = match.Groups[2].Value;
				
				var isWebDependency = Regex.Match(dependencyPath, UrlMatchPattern).Success;

				if (isWebDependency)
				{
					dependencies.Add(new WebDependency(dependencyPath){ IsNsDependency = isNsDependency });
				}
				else
				{
					dependencies.Add(new FileDependency(dependencyPath) { IsNsDependency = isNsDependency });
				}
			}

			return dependencies;
		}
	}
}