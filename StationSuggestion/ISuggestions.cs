using System.Collections.Generic;

namespace StationSuggestion
{
	public interface ISuggestions
	{
		IEnumerable<char> NextLetters { get; }
		IEnumerable<string> Stations { get; }
	}
}