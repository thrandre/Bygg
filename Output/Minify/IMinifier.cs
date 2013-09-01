using System;

namespace Bygg.Output.Minify
{
	public interface IMinifier
	{
		string Minify(string code);
	}
}