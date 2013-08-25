using Bygg.Dependencies.Path;

namespace Bygg.Dependencies
{
	public class WebDependency : Dependency
	{
		private readonly DependencyPath _path;

		public WebDependency(string path)
		{
			_path = new WebDependencyPath(path);
		}

		public override DependencyPath Path {
			get
			{
				return _path;
			}
		}
	}
}