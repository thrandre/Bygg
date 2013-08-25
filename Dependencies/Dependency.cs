using Bygg.Dependencies.Path;

namespace Bygg.Dependencies
{
	public abstract class Dependency
	{
		public bool IsNsDependency { get; set; }
		public abstract DependencyPath Path { get; }
	}
}