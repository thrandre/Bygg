using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bygg.Dependencies;

namespace Bygg.Input
{
	public class FakeReader : SourceCodeReader
	{
		private readonly FakeDependency _fake;

		public FakeReader(FakeDependency fake) : base(String.Empty)
		{
			_fake = fake;
		}

		public override IList<string> ReadLines()
		{
			return _fake.CodeLines;
		}
	}
}
