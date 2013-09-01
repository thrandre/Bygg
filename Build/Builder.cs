using System;
using System.Collections.Generic;
using System.Linq;
using Bygg.Dependencies;
using Bygg.Input;
using Bygg.Output.Combine;
using Bygg.Output.Minify;
using Bygg.Parser;
using Bygg.Sort;
using Bygg.Units;

namespace Bygg.Build
{
	public class Builder
	{
		public event ProgressEventHandler ProgressEvent;
		public delegate void ProgressEventHandler(object sender, ProgressEventHandlerArgs args);

		private readonly BuilderOptions _options;

		private string _namespace;
		private Dependency _namespaceDependency;
		private CodeUnit _rootUnit;
		private IList<CodeUnit> _orderedUnitList;

		private string _output;

		public Builder(BuilderOptions options)
		{
			_options = options;

			if (_options.RootDependency == null)
			{
				throw new ArgumentException("A root dependency must be provided.");
			}

			CodeUnit.Resolved += (sender, args) => 
				OnProgress(String.Format("Resolved {0}", args.Resolved));
		}

		public string Build()
		{
			GetNamespaceDependency();
			BuildRootUnit();
			SortDependencyGraph();
			GetNamespace();
			CombineOutput();

			if (_options.Minifier != null)
			{
				OnProgress("Minifying");
				_output = _options.Minifier.Minify(_output);
			}

			OnProgress("Done");

			return _output;
		}

		private void CombineOutput()
		{
			OnProgress("Combining output");
			_output = _options.OutputCombiner.Combine(_orderedUnitList, _namespace);
		}

		private void BuildRootUnit()
		{
			OnProgress("Building root unit");
			_rootUnit = new CodeUnit
				(
					_options.RootDependency, 
					_namespaceDependency, 
					_options.DependencyParser
				);

			OnProgress("Resolving dependencies");
			_rootUnit.ResolveDependencies();
		}

		private void SortDependencyGraph()
		{
			OnProgress("Sorting dependency graph");
			var sort = new TopologicalSort<CodeUnit>(_rootUnit, parent => parent.Dependencies);
			_orderedUnitList = sort.Sort();
		}

		private void GetNamespace()
		{
			var nsUnit = _orderedUnitList.FirstOrDefault
				(
					u => u.CurrentDependency.IsNamespaceDependency
				);
			
			if (nsUnit == null)
			{
				throw new ArgumentException("No valid namespace defined in root.");
			}
			
			_namespace = _options.NamespaceParser.Parse(nsUnit.CodeLines);
		}

		private void GetNamespaceDependency()
		{
			var rootDependencies = _options
				.DependencyParser.Parse
					(
						SourceCodeReader
							.GetReaderFor(_options.RootDependency)
							.ReadLines()
					);

			var nsDependency = rootDependencies
				.FirstOrDefault(d => d.IsNamespaceDependency);

			if (nsDependency == null)
			{
				throw new ArgumentException("No valid namespace defined in root.");
			}

			_namespaceDependency = nsDependency;
			_namespaceDependency.Path.MakePathAbsolute(_options.RootDependency.Path.Directory);
		}

		private void OnProgress(string message)
		{
			if (ProgressEvent != null)
			{
				ProgressEvent(this, new ProgressEventHandlerArgs(message));
			}
		}
	}
}
