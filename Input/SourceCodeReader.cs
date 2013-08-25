using System;
using System.Collections.Generic;
using Bygg.Dependencies;

namespace Bygg.Input
{
	public abstract class SourceCodeReader
	{
		public String Path { get; private set; }

		protected SourceCodeReader(String path)
		{
			Path = path;
		}

		public abstract IList<string> ReadLines();

		public static SourceCodeReader GetReaderFor(Dependency dependency)
		{
			if (dependency is WebDependency)
			{
				return new WebReader(dependency.Path.Path);
			}
			
			return new FileReader(dependency.Path.Path);
		}
	}
}