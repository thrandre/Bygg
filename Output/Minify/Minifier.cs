using System;

namespace Bygg.Output.Minify
{
	public class Minifier : IMinifier
	{
		private readonly Microsoft.Ajax.Utilities.Minifier _minifier;

		public Minifier()
		{
			_minifier = new Microsoft.Ajax.Utilities.Minifier();
		}

		public string Minify(string code)
		{
			return _minifier.MinifyJavaScript(code);
		}
	}
}