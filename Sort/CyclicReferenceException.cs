using System;

namespace Bygg.Sort
{
	public class CyclicReferenceException : Exception
	{
		private readonly object _contained;

		public CyclicReferenceException(object contained)
		{
			_contained = contained;
		}
	}
}