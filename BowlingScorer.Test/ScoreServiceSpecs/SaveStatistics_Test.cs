﻿using System.IO;
using System.Linq;
using System.Xml.Serialization;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace BowlingScorer.Test.ScoreServiceSpecs
{
	[TestFixture]
	public class SaveStatistics_Test
	{
		[Test]
		public void Should_allow_to_save_current_statistics()
		{
			// Arrange
			var repository = Substitute.For<IRepository>();
			var scorerService = new ScoreService(repository);

			// Act
			scorerService.Roll(5);
			scorerService.Roll(5);
			var statistics = scorerService.GetStatistics();
			scorerService.SaveStatistics();

			// Assert
			repository.Received(1)
					  .Save(Arg.Is<Frame[]>(s => 
						  s.First().FirstRoll == statistics.First().FirstRoll
						  && s.First().SecondRoll == statistics.First().SecondRoll));
		}

		[Test, Category("Integration")]
		public void Should_save_statistics_to_xml_file()
		{
			// Arrange
			var scoreService = new ScoreService(new XmlRepository());

			// Act
			scoreService.Roll(3);
			scoreService.SaveStatistics();

			// Assert
			string path = Path.Combine(Path.GetTempPath(), "statistics.xml");
			using (var reader = File.OpenText(path))
			{
				XmlSerializer serializer = new XmlSerializer(typeof(Frame[]));
				var statistics = (Frame[])serializer.Deserialize(reader);

				statistics.Should().ContainSingle(s => s.FirstRoll.HasValue, "only one record should be saved");

				reader.Close();
			}

			// Tear down
			File.Delete(path);
		}
	}
}