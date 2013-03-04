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
			Frame[] fakeStatistics = new Frame[10];
			fakeStatistics[0].FirstRoll = 10;
			fakeStatistics[1].FirstRoll = 5;
			fakeStatistics[1].SecondRoll = 1;

			using (var writer = File.OpenWrite(Path.Combine(Path.GetTempPath(), "statistics.xml")))
			{
				XmlSerializer serializer = new XmlSerializer(typeof(Frame[]));
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