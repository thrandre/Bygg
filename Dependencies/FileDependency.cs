using Bygg.Dependencies.Path;

namespace Bygg.Dependencies
{
	public class FileDependency : Dependency
	{
		public FileDependency(string path, bool isNamespaceDependency = false) 
			: base(new FileDependencyPath(path), isNamespaceDependency) { }
	}
}