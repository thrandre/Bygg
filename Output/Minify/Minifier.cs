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

		public String Minify(String code)
		{
			return _minifier.MinifyJavaScript(code);
		}
	}
}