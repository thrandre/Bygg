using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bygg.Input
{
	public class FileReader : SourceCodeReader
	{
		public FileReader(string path) : base(path) {}

		public override IList<String> ReadLines()
		{
			return File.ReadLines(Path).ToList();
		}
	}
}
