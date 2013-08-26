using System;
using System.Collections.Generic;
using System.Text;
using Bygg.Output.Transform;
using Bygg.Units;

namespace Bygg.Output.Combine
{
	public class OutputCombiner : IOutputCombiner
	{
		private readonly IOutputTransform _transform;

		public OutputCombiner(IOutputTransform transform)
		{
			_transform = transform;
		}

		public String Combine(IList<CodeUnit> units, String ns)
		{
			var code = new StringBuilder();

			foreach (var unit in units)
			{
				code.AppendLine
				(
					_transform.Transform
					(
						unit.CodeLines,
						ns, 
						(
							unit.CurrentDependency.IsNsDependency
							|| unit.IsLibrary
						)
					)
				);
			}

			return code.ToString();
		}
	}
}
