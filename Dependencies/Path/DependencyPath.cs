using System;

namespace Bygg.Dependencies.Path
{
	public abstract class DependencyPath : IEquatable<DependencyPath>
	{
		public string Path { get; protected set; }
		
		public abstract string Directory { get; }

		protected DependencyPath(string path)
		{
			Path = path;
		}

		public abstract void MakePathAbsolute(string parentPath);
		
		public bool Equals(DependencyPath other)
		{
			return Path == other.Path;
		}
	}
}