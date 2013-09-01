using System;
using System.Collections.Generic;
using System.Linq;
using Bygg.Dependencies;
using Bygg.Input;
using Bygg.Parser;

namespace Bygg.Units
{
	public class CodeUnit : IEquatable<CodeUnit>
	{
		public static event ResolvedEvent Resolved;
		public delegate void ResolvedEvent(object sender, ResolvedEventArgs args);

		private void OnResolve(string resolved)
		{
			if (Resolved != null)
			{
				Resolved(this, new ResolvedEventArgs(resolved));
			}
		}

		private readonly DependencyParser _parser;
		private readonly Dependency _namespaceDependency;

		public readonly Dependency CurrentDependency;
		public IList<string> CodeLines { get; private set; }
		public readonly IList<CodeUnit> Dependencies = new List<CodeUnit>();

		public bool IsNamespaceDependency { 
			get
			{
				return CurrentDependency.IsNamespaceDependency;
			} 
		}
		
		public bool IsLibrary { get; private set; }

		public CodeUnit(Dependency currentDependency, Dependency namespaceDependency, DependencyParser parser, bool isLibrary = false)
		{
			_namespaceDependency = namespaceDependency;
			_parser = parser;

			CurrentDependency = currentDependency;
			IsLibrary = isLibrary;
		}

		public void ResolveDependencies(IList<CodeUnit> resolvedUnits = null)
		{
			if (resolvedUnits == null)
			{
				resolvedUnits = new List<CodeUnit>();
			}

			var reader = SourceCodeReader.GetReaderFor(CurrentDependency);
			CodeLines = reader.ReadLines();

			var dependencies = _parser.Parse(CodeLines);

			if (!IsNamespaceDependency && !IsLibrary)
			{
				dependencies.Add(_namespaceDependency);
			}

			foreach (var dependency in dependencies)
			{
				dependency.Path.MakePathAbsolute(CurrentDependency.Path.Directory);

				var resolved = resolvedUnits.FirstOrDefault(u => u.CurrentDependency.Path.Equals(dependency.Path));

				if (resolved != null)
				{
					Dependencies.Add(resolved);
				}
				else
				{
					var newUnit = new CodeUnit(dependency, _namespaceDependency, _parser, CurrentDependency.IsNamespaceDependency);

					Dependencies.Add(newUnit);
					resolvedUnits.Add(newUnit);

					OnResolve(newUnit.CurrentDependency.Path.Path);

					newUnit.ResolveDependencies(resolvedUnits);
				}
			}
		}

		public bool Equals(CodeUnit other)
		{
			return CurrentDependency.Path.Equals(other.CurrentDependency.Path);
		}
	}

	public class ResolvedEventArgs : EventArgs
	{
		public string Resolved { get; private set; }

		public ResolvedEventArgs(string resolved)
		{
			Resolved = resolved;
		}
	}
}