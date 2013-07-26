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
			Assert.That(suggestion.nextLetters.Contains('F'));
			Assert.That(suggestion.nextLetters.Contains('M'));

			// Check the suggested stations are correct.
			Assert.That(suggestion.stations.Contains("DARTFORD"));
			Assert.That(suggestion.stations.Contains("DARTMOUTH"));
		}
	}
}
