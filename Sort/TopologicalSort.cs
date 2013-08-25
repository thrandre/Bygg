using System;
using System.Collections.Generic;
using System.Linq;

namespace Bygg.Sort
{
	public class TopologicalSort<T> where T : IEquatable<T>
	{
		private readonly Func<T, IList<T>> _getChildren;
		private readonly TopologicalNode<T> _root;

		private readonly IList<TopologicalNode<T>> _nodePool;  
		
		public TopologicalSort(T root, Func<T, IList<T>> getChildren)
		{
			_getChildren = getChildren;
			_root = new TopologicalNode<T> { Contained = root };
			_nodePool = new List<TopologicalNode<T>> { _root };
		}

		private IEnumerable<TopologicalNode<T>> GetChildNodes(TopologicalNode<T> node)
		{
			var children = new List<TopologicalNode<T>>();
			var rawChildren = _getChildren(node.Contained);
			foreach (var child in rawChildren)
			{
				var resolved = _nodePool.FirstOrDefault(n => n.Contained.Equals(child));
				if (resolved != null)
				{
					children.Add(resolved);
				}
				else
				{
					var newNode = new TopologicalNode<T> { Contained = child };
					children.Add(newNode);
					
					_nodePool.Add(newNode);
				}
			}
			
			return children;
		}

		private void Visit(TopologicalNode<T> node, ICollection<T> stack)
		{
			if (node.TempMarked)
			{
				throw new CyclicReferenceException(node.Contained);
			}
			
			if (node.Marked)
			{
				return;
			}
			
			node.TempMarked = true;
			
			var children = GetChildNodes(node);
			foreach (var child in children)
			{
				Visit(child, stack);
			}
			
			node.Marked = true;
			node.TempMarked = false;
			stack.Add(node.Contained);
		}

		public IList<T> Sort()
		{
			var stack = new List<T>();
			Visit(_root, stack);
			
			return stack;
		}
	}
}