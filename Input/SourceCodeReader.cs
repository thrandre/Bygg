﻿using System.Collections.Generic;
using Bygg.Dependencies;

namespace Bygg.Input
{
	public abstract class SourceCodeReader
	{
		public string Path { get; private set; }

		protected SourceCodeReader(string path)
		{
			Path = path;
		}

		public abstract IList<string> ReadLines();

		public static SourceCodeReader GetReaderFor(Dependency dependency)
		{
			if (dependency is FakeDependency)
			{
				return new FakeReader(dependency as FakeDependency);	
			}
			
			if (dependency is WebDependency)
			{
				return new WebReader(dependency.Path.Path);
			}
			
			return new FileReader(dependency.Path.Path);
		}
	}
}