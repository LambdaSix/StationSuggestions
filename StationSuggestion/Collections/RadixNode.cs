using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace StationSuggestion.Collections
{
	public interface IRadixNode<T, U>
	{
		T Key { get; set; }
		U Value { get; set; }
		bool IsTerminal { get; set; }

		IEnumerable<IRadixNode<T, U>> Children { get; set; }
	}

	public class RadixNode : IRadixNode<string,string>, IEnumerable<RadixNode>
	{
		private readonly List<RadixNode> _children;
		public string Key { get; set; }
		public string Value { get; set; }
		public bool IsTerminal { get; set; }

		public IEnumerable<IRadixNode<string,string>> Children
		{
			get { return _children; }
			set {  }
		}

		public RadixNode(string key, string value, bool isTerminal )
		{
			Key = key;
			Value = value;
			IsTerminal = isTerminal;
			_children = new List<RadixNode>();
		}

		public RadixNode(char key, string value, bool isTerminal) : this(key.ToString(), value, isTerminal)
		{ }

		public void AddChild(RadixNode node)
		{
			_children.Add(node);
		}

		public IEnumerator<RadixNode> GetEnumerator()
		{
			return (from item in Children where item != null select item as RadixNode).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public IEnumerable<RadixNode> GetTerminals()
		{
			return GetChildren().Where(x => x.IsTerminal);
		}

		private IEnumerable<RadixNode> GetChildren()
		{
			var stack = new Stack<RadixNode>(_children);

			while (stack.Count > 0)
			{
				var nextNode = stack.Pop();
				foreach (var node in nextNode.Children as IEnumerable<RadixNode>)
				{
					yield return node;

					if (node.Any())
					{
						stack.Push(node);
					}
				}
			}
		}

		public IEnumerable<char> GetSuggestions()
		{
			return Children.SelectMany(x => x.Key);
		}
	}
}