using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using Bygg.Build;
using Bygg.Dependencies;

namespace Bygg.Bundle
{
	public class ByggBundler : IBundleTransform
	{
		public ByggBundler()
		{
			
		}

		public void Process(BundleContext context, BundleResponse response)
		{
			if (response.Files.Count() != 1)
			{
				return;
			}

			var rootFile = response.Files.First();
			var rootDependency = new FileDependency(rootFile.FullName);

			var builder = new Builder(
				new BuilderOptions
					{
						RootDependency = rootDependency
					});

			builder.ProgressEvent += (sender, args) => Debug.WriteLine(args.Message);

			var content = builder.Build(true);

			response.ContentType = "text/javascript";
			response.Cacheability = HttpCacheability.NoCache;
			response.Content = content;
		}
	}
}
