using System;

namespace Bygg.Output.Minify
{
	public interface IMinifier
	{
		String Minify(String code);
	}
}