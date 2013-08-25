using System;

namespace Bygg.Sort
{
	public class TopologicalNode<T> : IEquatable<TopologicalNode<T>>
	{
		public bool Marked { get; set; }
		public bool TempMarked { get; set; }
		public T Contained { get; set; }
		
		public bool Equals(TopologicalNode<T> other)
		{
			return Contained.Equals(other.Contained);
		}
	}
}