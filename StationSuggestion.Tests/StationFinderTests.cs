using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace StationSuggestion.Tests
{
	[TestFixture]
	class StationFinderTests
	{
		private StationFinder _finder;

		[SetUp]
		public void Setup()
		{
			_finder = new StationFinder();
		}

		[Test]
		public void GetSuggestionsShouldReturnValues()
		{
			var suggestion = _finder.GetSuggestions("DART");

			// Check the suggested letters are correct.
			Assert.That(suggestion.NextLetters.Contains('F'));
			Assert.That(suggestion.NextLetters.Contains('M'));

			// Check the suggested stations are correct.
			Assert.That(suggestion.Stations.Contains("DARTFORD"));
			Assert.That(suggestion.Stations.Contains("DARTMOUTH"));
		}

		[Test]
		public async Task GetSuggestionsAsyncShouldReturnValues()
		{
			var suggestion = await _finder.GetSuggestionsAsync("DART");

			// Check the suggested letters are correct.
			Assert.That(suggestion.NextLetters.Contains('F'));
			Assert.That(suggestion.NextLetters.Contains('M'));

			// Check the suggested stations are correct.
			Assert.That(suggestion.Stations.Contains("DARTFORD"));
			Assert.That(suggestion.Stations.Contains("DARTMOUTH"));
		}

		[Test]
		public void GetSuggestionsOrDefaultShouldReturnValues()
		{
			var suggestion = _finder.GetSuggestionsOrDefault("Xy");

			Assert.That(suggestion == null);
		}

		[Test]
		public void GetSuggestionsShouldThrow()
		{
			Assert.Throws<KeyNotFoundException>(() => _finder.GetSuggestions("Xy"));
		}
	}
}
