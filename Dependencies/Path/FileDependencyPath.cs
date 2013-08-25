using System;
using System.IO;

namespace Bygg.Dependencies.Path
{
	public class FileDependencyPath : DependencyPath
	{
		public FileDependencyPath(String path) : base(path) {}

		public override String Directory
		{
			get
			{
				var directoryInfo = new FileInfo(Path).Directory;
				return directoryInfo != null ? directoryInfo.FullName : String.Empty;
			}
		}

		public override void MakePathAbsolute(string parentPath)
		{
			Path = new FileInfo(System.IO.Path.Combine(parentPath, Path)).FullName;
		}
	}
}