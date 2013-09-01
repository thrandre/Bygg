using System;
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
		private readonly ByggBundlerOptions _options;

		public ByggBundler(ByggBundlerOptions options)
		{
			_options = options;
		}

		public void Process(BundleContext context, BundleResponse response)
		{
			if (response.Files.Count() != 1)
			{
				return;
			}

			var rootFile = response.Files.First();
			var rootFilePath = HttpContext.Current.Server.MapPath(rootFile.IncludedVirtualPath);
			
			_options.RootDependency = new FileDependency(rootFilePath);

			var builder = new Builder(_options);

			builder.ProgressEvent += (sender, args) => Debug.WriteLine(args.Message);

			var content = builder.Build();

			response.ContentType = "text/javascript";
			response.Cacheability = HttpCacheability.Public;
			response.Content = content;
		}
	}
}
