using Bygg.Dependencies.Path;

namespace Bygg.Dependencies
{
	public class FileDependency : Dependency
	{
		private readonly DependencyPath _path;

		public FileDependency(string path)
		{
			_path = new FileDependencyPath(path);
		}

		public override DependencyPath Path {
			get
			{
				return _path;
			}
		}
	}
}