using Bygg.Dependencies;
using Bygg.Output.Combine;
using Bygg.Output.Minify;
using Bygg.Parser;

namespace Bygg.Build
{
	public class BuilderOptions
	{
		public Dependency RootDependency { get; set; }
		public DependencyParser DependencyParser { get; set; }
		public NamespaceParser NamespaceParser { get; set; }
		public IOutputCombiner OutputCombiner { get; set; }
		public IMinifier Minifier { get; set; }
	}
}