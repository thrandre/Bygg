namespace Bygg
{
	public static class Settings
	{
		public const string NamespaceMatchPattern = @"(?:var)?[\s]?(.*?)[\s]?=[\s]?(?:.*?);";
		public const string DependencyMatchPattern = @"///[\s]?(dep|ns)[\s]?:[\s]?((.*?)\.js)";
	}
}