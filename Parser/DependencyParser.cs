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

				var isNsDependency = match.Groups[1].Value == "ns";
				var dependencyPath = match.Groups[2].Value;

				var isFakeDependency = !Regex.Match(dependencyPath, FileMatchPattern).Success;

				if (isFakeDependency)
				{
					dependencies.Add
						(
							new FakeDependency(new List<string>{ dependencyPath })
								{
									IsNamespaceDependency = isNsDependency
								}
						);
				}
				else
				{
					var isWebDependency = Regex.Match(dependencyPath, UrlMatchPattern).Success;

					if (isWebDependency)
					{
						dependencies.Add(new WebDependency(dependencyPath) { IsNamespaceDependency = isNsDependency });
					}
					else
					{
						dependencies.Add(new FileDependency(dependencyPath) { IsNamespaceDependency = isNsDependency });
					}	
				}
			}

			return dependencies;
		}
	}
}