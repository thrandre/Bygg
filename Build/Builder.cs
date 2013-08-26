using System;
using System.Collections.Generic;
using System.Linq;
using Bygg.Dependencies;
using Bygg.Input;
using Bygg.Output.Combine;
using Bygg.Output.Format;
using Bygg.Output.Minify;
using Bygg.Output.Transform;
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

		private String _namespace;
		private Dependency _namespaceDependency;
		private CodeUnit _rootUnit;
		private IList<CodeUnit> _orderedUnitList;

		private String _output;

		public Builder(BuilderOptions options)
		{
			_options = options;

			if (_options.RootDependency == null)
			{
				throw new ArgumentException("A root dependency must be provided.");
			}

			if (_options.DependencyParser == null)
			{
				_options.DependencyParser = new DependencyParser(Settings.DependencyMatchPattern);
			}

			if (_options.NamespaceParser == null)
			{
				_options.NamespaceParser = new NamespaceParser(Settings.NamespaceMatchPattern);
			}

			if (_options.OutputCombiner == null)
			{
				_options.OutputCombiner = new OutputCombiner
				(
					new JsCommonsTransform
					(
						new OutputFormatter()
					)
				);
			}

			if (_options.Minifier == null)
			{
				_options.Minifier = new Minifier();
			}

			CodeUnit.Resolved += (sender, args) => OnProgress(args.Resolved);
		}

		public String Build(bool minify = false)
		{
			GetNamespaceDependency();
			BuildRootUnit();
			SortDependencyGraph();
			GetNamespace();
			CombineOutput();

			if (minify)
			{
				_output = _options.Minifier.Minify(_output);
			}

			return _output;
		}

		private void CombineOutput()
		{
			_output = _options.OutputCombiner.Combine(_orderedUnitList, _namespace);
		}

		private void BuildRootUnit()
		{
			_rootUnit = new CodeUnit(
				_options.RootDependency, 
				_namespaceDependency, 
				_options.DependencyParser
			);
			
			_rootUnit.ResolveDependencies();
		}

		private void SortDependencyGraph()
		{
			var sort = new TopologicalSort<CodeUnit>(_rootUnit, parent => parent.Dependencies);
			_orderedUnitList = sort.Sort();
		}

		private void GetNamespace()
		{
			var nsUnit = _orderedUnitList.FirstOrDefault(u => u.CurrentDependency.IsNsDependency);
			if (nsUnit != null)
			{
				_namespace = _options.NamespaceParser.Parse(nsUnit.CodeLines);
			}
		}

		private void GetNamespaceDependency()
		{
			var rootDependencies = _options
				.DependencyParser.Parse(
					SourceCodeReader
						.GetReaderFor(_options.RootDependency)
						.ReadLines());

			var nsDependency = rootDependencies
				.FirstOrDefault(d => d.IsNsDependency);

			if (nsDependency == null) return;

			_namespaceDependency = nsDependency;
			_namespaceDependency.Path.MakePathAbsolute(_options.RootDependency.Path.Directory);
		}

		private void OnProgress(String message)
		{
			if (ProgressEvent != null)
			{
				ProgressEvent(this, new ProgressEventHandlerArgs(message));
			}
		}
	}
}
