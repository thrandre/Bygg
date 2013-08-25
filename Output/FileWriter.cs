using System;
using System.IO;

namespace Bygg.Output
{
	public class FileWriter
	{
		private readonly string _path;

		public FileWriter(String path)
		{
			_path = path;
		}

		public void Write(String code)
		{
			File.WriteAllText(_path, code);
		}
	}
}