using System.Collections.Generic;
using Bygg.Output.Format;
using Bygg.Output.Transform;
using Bygg.Units;

namespace Bygg.Output.Combine
{
	public class OutputCombinerOptions
	{
		public IOutputTransform UnitTransform { get; set; }
		public IOutputTransform CodeTransform { get; set; }
		public IOutputFormatter OutputFormatter { get; set; }
	}

	public class OutputCombiner : IOutputCombiner
	{
		private readonly OutputCombinerOptions _options;

		public OutputCombiner(OutputCombinerOptions options = null)
		{
			_options = options ?? new OutputCombinerOptions();
		}

		public string Combine(IEnumerable<CodeUnit> units, string ns)
		{
			var code = new List<string>();

			foreach (var unit in units)
			{
				var lines = unit.CodeLines;

				if ((!unit.IsNamespaceDependency && !unit.IsLibrary) && _options.UnitTransform != null)
				{
					lines = _options.UnitTransform.Transform(lines, ns);
				}

				if ((!unit.IsNamespaceDependency && !unit.IsLibrary) && _options.OutputFormatter != null)
				{
					lines = _options.OutputFormatter.Format(lines);
				}

				code.AddRange(lines);
			}

			return string.Join("\n", code);
		}
	}
}
