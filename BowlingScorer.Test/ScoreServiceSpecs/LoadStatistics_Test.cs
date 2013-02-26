using System.IO;
using System.Xml.Serialization;
using FluentAssertions;
using NUnit.Framework;

namespace BowlingScorer.Test.ScoreServiceSpecs
{
	[TestFixture]
	public class LoadStatistics_Test
	{
		[Test, Category("Integration")]
		public void Should_load_statistics_from_file()
		{
			// Arrange
			var service = new ScoreService(new XmlRepository());
			int?[] fakeStatistics = new int?[21];
			fakeStatistics[0] = 10;
			fakeStatistics[2] = 5;
			fakeStatistics[3] = 1;

			using (var writer = File.OpenWrite(Path.Combine(Path.GetTempPath(), "statistics.xml")))
			{
				XmlSerializer serializer = new XmlSerializer(typeof(int?[]));
				serializer.Serialize(writer, fakeStatistics);
				writer.Close();
			}

			// Act
			service.LoadStatistics();
			var score = service.CalculateScore();

			// Assert
			score.Should().Be(22);
		}
	}
}