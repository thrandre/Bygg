using Bygg.Dependencies.Path;

namespace Bygg.Dependencies
{
	public abstract class Dependency
	{
		public bool IsNamespaceDependency { get; protected set; }
		public DependencyPath Path { get; protected set; }

		protected Dependency(DependencyPath path, bool isNamespaceDependency)
		{
			Path = path;
			IsNamespaceDependency = isNamespaceDependency;
		}
	}
}