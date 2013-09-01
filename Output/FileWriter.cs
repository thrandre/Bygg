using System;
using System.IO;

namespace Bygg.Output
{
	public class FileWriter
	{
		private readonly string _path;

		public FileWriter(string path)
		{
			_path = path;
		}

		public void Write(string code)
		{
			File.WriteAllText(_path, code);
		}
	}
}