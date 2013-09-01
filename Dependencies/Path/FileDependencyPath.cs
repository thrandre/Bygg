using System;
using System.IO;

namespace Bygg.Dependencies.Path
{
	public class FileDependencyPath : DependencyPath
	{
		public FileDependencyPath(string path) : base(path) {}

		public override string Directory
		{
			get
			{
				var directoryInfo = new FileInfo(Path).Directory;
				return directoryInfo != null ? directoryInfo.FullName : string.Empty;
			}
		}

		public override void MakePathAbsolute(string parentPath)
		{
			Path = new FileInfo(System.IO.Path.Combine(parentPath, Path)).FullName;
		}
	}
}