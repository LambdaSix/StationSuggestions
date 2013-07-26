using System.Collections.Generic;

namespace StationSuggestion.Collections
{
	/// <summary>
	/// Interface contract for RadixTrees
	/// </summary>
	/// <typeparam name="T">Key type</typeparam>
	/// <typeparam name="U">Node type</typeparam>
	public interface IRadixTree<in T,U>
	{
		IEnumerable<U> Root { get; set; }
		void Add(T key);
		U Retrieve(IEnumerable<char> key);
	}
}
