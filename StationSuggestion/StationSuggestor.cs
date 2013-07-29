using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StationSuggestion.Collections;

namespace StationSuggestion
{
	public class Suggestions : ISuggestions
	{
		private RadixNode _node;

		public Suggestions(RadixNode node)
		{
			_node = node;
		}

		public IEnumerable<char> NextLetters
		{
			get { return _node.GetSuggestions(); }
		}

		public IEnumerable<String> Stations
		{
			get { return _node.GetTerminals().Select(x => x.Value); }
		}
	}

	public interface IStationFinder
	{
		ISuggestions GetSuggestions(string userInput);
	}

	public class StationFinder : IStationFinder
	{
		private static readonly List<string> stations = new List<string>()
			{
				"LIVERPOOL LIME STREET",
				"BIRMINGHAM NEW STREET",
				"KINGSTON",
				"DARTFORD",
				"DARTMOUTH"
			};

		private readonly RadixTree _map;

		/// <summary>
		/// Construct a new tree with the default stations.
		/// </summary>
		public StationFinder() : this(stations)
		{ }

		/// <summary>
		/// Construct a new tree with the given list of stations.
		/// </summary>
		/// <param name="terminalNodes">A enumerable colletion of strings.</param>
		public StationFinder(IEnumerable<string> terminalNodes )
		{
			_map = new RadixTree(terminalNodes);
		}

		/// <summary>
		/// Retrieve the possible stations and next letters for the given input.
		/// </summary>
		/// <param name="userInput">Input to match on.</param>
		/// <returns>A <seealso cref="ISuggestions"/> object.</returns>
		public ISuggestions GetSuggestions(string userInput)
		{
			return new Suggestions(_map.Retrieve(userInput));
		}

		/// <summary>
		/// Retrieve the possible stations and next letters for the given input.
		/// </summary>ISuggestion
		/// <param name="userInput">Input to match on</param>
		/// <returns>A <seealso cref="ISuggestions"/> object.</returns>
		public async Task<ISuggestions> GetSuggestionsAsync(string userInput)
		{
			return await Task.Run(() => new Suggestions(_map.Retrieve(userInput)));
		}

		/// <summary>
		/// Retrieve the possible stations and next letters for the given input,
		/// or null if station not found.
		/// </summary>
		/// <param name="userInput"></param>
		/// <returns></returns>
		public ISuggestions GetSuggestionsOrDefault(string userInput)
		{
			try
			{
				return GetSuggestions(userInput);
			}
			catch (KeyNotFoundException ex)
			{
				return null;
			}
		}
	}

}