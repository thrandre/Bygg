using Bygg.Dependencies.Path;

namespace Bygg.Dependencies
{
	public abstract class Dependency
	{
		public bool IsNamespaceDependency { get; set; }
		public abstract DependencyPath Path { get; }
	}
}