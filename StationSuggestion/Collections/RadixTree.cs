using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace StationSuggestion.Collections
{
	/// <summary>
	/// Implementation of a RadixTree.
	/// 
	/// While a RadixTree can be enumerated, it will recurse over all possible nodes.
	/// </summary>
	public class RadixTree : IRadixTree<string, RadixNode>, IEnumerable<RadixNode>
	{
		private readonly List<RadixNode> _root;

		/// <summary>
		/// Root node of the tree.
		/// </summary>
		public IEnumerable<RadixNode> Root
		{
			get { return _root; }
		}

		public RadixTree()
		{
			_root = new List<RadixNode>();
		}

		public RadixTree(IEnumerable<string> stationList)
		{
			_root = new List<RadixNode>();

			foreach (var item in stationList)
			{
				Add(item);
			}
		}

		public RadixTree(IEnumerable<RadixNode> collection)
		{
			_root = new List<RadixNode>(collection);
		}

		/// <summary>
		/// Add the given word to the RadixTree.
		/// </summary>
		/// <param name="value">Word value to add to the tree.</param>
		public void Add(string value)
		{
			// Convert the key to an array of characters.
			var array = value.ToArray();

			// Check if we have the first letter in the root collection.
			if ( !Root.Any(x => x.Key == array[0].ToString()) )
			{
				// Add the first letter to the top-level collection.
				_root.Add(new RadixNode(array[0], array[0].ToString(), isTerminal: false));
			}

			// Pull the branch that has the first letter.
			var currentNode = _root.Single(x => x.Key == array[0].ToString());

			// For each character, except the first
			foreach (var item in array.Skip(1))
			{
				// If the node we're looking at doesnt contain the character we're considering:
				if (!currentNode.Children.Any(x => x.Key == item.ToString()))
				{
					// Add it:
					currentNode.AddChild(new RadixNode(item, String.Concat(currentNode.Value, item), isTerminal: false));
				}

				// Otherwise, move down a node for the next character.
				currentNode = currentNode.First(x => x.Key == item.ToString());
			}

			// Once we've added a node for each character, mark the final node as terminal.
			currentNode.IsTerminal = true;
		}

		/// <summary>
		/// Retrieve the given key from the radix tree.
		/// 
		/// Partial matches are supported.
		/// </summary>
		/// <param name="key">String to search for. Partial matching works.</param>
		/// <returns>The first <seealso cref="RadixNode"/> that matches the key.</returns>
		public RadixNode Retrieve(IEnumerable<char> key)
		{
			var array = key.ToArray();
			var currentNode = Root.FirstOrDefault(x => x.Key == array[0].ToString());

			if (currentNode == null)
			{
				throw new KeyNotFoundException("Key was not found in RadixTree");
			}

			for (int i = 1; i < array.Count() && currentNode != null; i++)
			{
				currentNode = currentNode.Children.FirstOrDefault(x => x.Key == array[i].ToString()) as RadixNode;

				if (currentNode == null)
				{
					return null;
				}
			}

			return currentNode;
		}

		/// <summary>
		/// Returns a flatmap of the entire tree.
		/// </summary>
		/// <returns>Flattened representation of the tree.</returns>
		public IEnumerator<RadixNode> GetEnumerator()
		{
			var stack = new Stack<RadixNode>(Root);
			
			while (stack.Count > 0)
			{
				var nextRadix = stack.Pop();

				foreach (RadixNode node in nextRadix.Children)
				{
					yield return node;

					if (node.Any())
						stack.Push(node);
				}
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}