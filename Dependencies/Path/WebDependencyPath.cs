using System;

namespace Bygg.Dependencies.Path
{
	public class WebDependencyPath : DependencyPath
	{
		public WebDependencyPath(string path) : base(path) {}

		public override String Directory
		{
			get { return new Uri(Path).AbsoluteUri; }
		}

		public override void MakePathAbsolute(String parentPath)
		{
		}
	}
}