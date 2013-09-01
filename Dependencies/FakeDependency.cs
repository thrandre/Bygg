using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bygg.Dependencies.Path;

namespace Bygg.Dependencies
{
	public class FakeDependency : Dependency
	{
		public override DependencyPath Path
		{
			get
			{
				return new FileDependencyPath(String.Empty);
			}
		}

		public IList<string> CodeLines { get; private set; } 

		public FakeDependency(IEnumerable<string> codeLines)
		{
			CodeLines = codeLines.ToList();
		}
	}
}
