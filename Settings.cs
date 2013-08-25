using System;

namespace Bygg
{
	public class Settings
	{
		public const String NamespaceMatchPattern = @"(?:var)?[\s]?(.*?)[\s]?=[\s]?(?:.*?);";
		public const String DependencyMatchPattern = @"///[\s]?(dep|ns)[\s]?:[\s]?((.*?)\.js)";
	}
}