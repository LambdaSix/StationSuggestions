using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StationSuggestion.Collections;

namespace StationSuggestion
{
	public class Suggestions
	{
		private RadixNode _node;

		public Suggestions(RadixNode node)
		{
			_node = node;
		}

		public IEnumerable<char> nextLetters
		{
			get { return _node.GetSuggestions(); }
		}

		public IEnumerable<String> stations
		{
			get { return _node.GetTerminals().Select(x => x.Value); }
		}
	}

	public class StationFinder
	{
		private static List<string> stations = new List<string>()
			{
				"LIVERPOOL LIME STREET",
				"BIRMINGHAM NEW STREET",
				"KINGSTON",
				"DARTFORD",
				"DARTMOUTH"
			};

		private readonly RadixTree _map;

		public StationFinder()
		{
			_map = new RadixTree(stations);
		}

		public Suggestions GetSuggestions(string userInput)
		{
			return new Suggestions(_map.Retrieve(userInput));
		}
	}

}