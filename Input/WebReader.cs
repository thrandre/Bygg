using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Bygg.Input
{
	public class WebReader : SourceCodeReader
	{
		public WebReader(string path) : base(path) {}

		public override IList<String> ReadLines()
		{
			String code;
			using (var client = new WebClient())
			{
				code = client.DownloadString(Path);
			}

			return code.Split('\n').ToList();
		}
	}
}