using System;

namespace Bygg.Dependencies.Path
{
	public abstract class DependencyPath : IEquatable<DependencyPath>
	{
		public String Path { get; protected set; }
		
		public abstract String Directory { get; }

		protected DependencyPath(String path)
		{
			Path = path;
		}

		public abstract void MakePathAbsolute(String parentPath);
		
		public bool Equals(DependencyPath other)
		{
			return Path == other.Path;
		}
	}
}