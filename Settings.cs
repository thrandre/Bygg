namespace Bygg
{
	public static class Settings
	{
		public const string NamespaceMatchPattern = @"(?:var)?[\s]?(.*?)[\s]?=[\s]?(?:.*?);";
		public const string DependencyMatchPattern = @"///[\s]?<reference path=""((.*?)\.js)""[\s]?(ns=""(true|false)"")?[\s]?/>";
	}
}