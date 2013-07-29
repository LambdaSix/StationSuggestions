using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using StationSuggestion.Collections;

namespace StationSuggestion.Tests.Collections
{
	[TestFixture(Description = "A RadixTree should")]
	public class RadixTreeTests
	{
		private RadixTree _map;

		private readonly IEnumerable<string> stationList = new string[]
			{
					"Birmingham", "Bristol", "Brighton",
					"Manchester", "London", "Ipswitch",
					"Glasgow", "Edinburgh", "Whitburn",
					"Norwich", "Bath", "Wells",
					"Oxford", "Cambridge", "Auchtermuchty",
					"Aberdeen", "Croydon", "Leeds",
					"Liverpool", "Sheffield", "Swansea",
					"Swindon", "Newport", "Cardiff",
					"Kent", "Yorkshire", "Canterbury"
			};
			
		[SetUp]
		public void Setup()
		{
			_map = new RadixTree(stationList);
		}

		[Test]
		public void ShouldReturnTerminalStationsFromSingleLetter()
		{
			var stations = _map.Retrieve("C").GetTerminals().Select(x => x.Value).ToList();
			Assert.That(stations.Contains("Cambridge"));
			Assert.That(stations.Contains("Croydon"));
			Assert.That(stations.Contains("Canterbury"));

			stations = _map.Retrieve("N").GetTerminals().Select(x => x.Value).ToList();
			Assert.That(stations.Contains("Newport"));
		}

		[Test]
		public void ShouldReturnTerminalStations()
		{
			var stations = _map.Retrieve("Ca").GetTerminals().Select(x => x.Value).ToList();
			Assert.That(stations.Contains("Cambridge"));
			Assert.That(stations.Contains("Canterbury"));

			stations = _map.Retrieve("Sw").GetTerminals().Select(x => x.Value).ToList();
			Assert.That(stations.Contains("Swansea"));
			Assert.That(stations.Contains("Swindon"));
		}

		[Test]
		public void ShouldReturnSuggestionsForPrefixes()
		{
			var suggestions = _map.Retrieve("Ca").GetSuggestions().ToList();
			Assert.That(suggestions.Contains('m'));
			Assert.That(suggestions.Contains('n'));
		}

		[Test]
		public void ShouldThrowExceptionForNonMatches()
		{
			Assert.Throws<KeyNotFoundException>(() =>
				{
					_map.Retrieve("Xy").GetTerminals().Select(x => x.Value).ToList();
				});
		}
	}
}
