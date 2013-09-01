using Bygg.Build;
using Bygg.Output.Combine;
using Bygg.Output.Format;
using Bygg.Output.Transform;
using Bygg.Parser;

namespace Bygg.Bundle
{
	public class ByggBundlerOptions : BuilderOptions
	{
		public static ByggBundlerOptions Default
		{
			get
			{
				return new ByggBundlerOptions
					{
						DependencyParser = new DependencyParser(Settings.DependencyMatchPattern),
						NamespaceParser = new NamespaceParser(Settings.NamespaceMatchPattern),
						OutputCombiner = new OutputCombiner(
							new OutputCombinerOptions
								{
									UnitTransform = new JsCommonsTransform(),
									OutputFormatter = new StandardIndentationFormatter()
								})
					};
			}
		}
	}
}