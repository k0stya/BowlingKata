using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace BowlingScorer.Test.ScorerSpecs
{
	[TestFixture]
	public class GetStatistics_Test
	{
		[Test]
		public void Should_be_10_frames()
		{
			// Arrange
			var scorer = new Scorer();

			// Act
			Frame[] statistics = scorer.GetStatistics();

			// Assert
			statistics.Should().HaveCount(10, "there are 10 frames");
		}

		[Test]
		public void Should_contain_first_frame_with_all_details_when_rolling_two_times()
		{
			// Arrange
			var scorer = new Scorer();

			// Act
			scorer.Roll(3);
			scorer.Roll(3);
			Frame[] statistics = scorer.GetStatistics();

			// Assert
			statistics.First().FirstRoll.Should().Be(3, "first roll");
			statistics.First().SecondRoll.Should().Be(3, "second roll");
			statistics.First().Total.Should().Be(6, "total");
		}

		[Test]
		public void Should_not_calculate_total_for_spare_until_next_roll_is_done()
		{
			// Arrange
			var scorer = new Scorer();

			// Act
			scorer.Roll(5);
			scorer.Roll(5);
			var statistics = scorer.GetStatistics();

			// Assert
			statistics.First().Total.Should().Be(null, "frame's total should not be caclulated until next roll is done");
		}

		[Test]
		public void Should_calculate_spare_when_subsequent_roll_is_done()
		{
			// Arrange
			var scorer = new Scorer();

			// Act
			scorer.Roll(3);
			scorer.Roll(7);
			scorer.Roll(3);
			var statistics = scorer.GetStatistics();
			
			// Assert
			statistics.First().Total.Should().Be(13, "should sum subsuquent roll is spare");
		}

		[Test]
		public void Should_not_calculate_strike_until_two_subsuquent_rolls_are_done()
		{
			// Arrange
			var scorer = new Scorer();

			// Act
			scorer.Roll(10);
			scorer.Roll(2);
			var statistics = scorer.GetStatistics();

			// Assert
			statistics.First().Total.HasValue.Should().BeFalse("frame's total should not be calculated until two subsequent rolls are done");
			statistics.First().SecondRoll.HasValue.Should().BeFalse("second roll should be empty if strike");
		}

		[Test]
		public void Should_sum_two_subsequent_rolls_if_strike()
		{
			// Arrange
			var scorer = new Scorer();

			// Act
			scorer.Roll(10);
			scorer.Roll(1);
			scorer.Roll(3);
			var statistics = scorer.GetStatistics();

			// Assert
			statistics.First().Total.Should().Be(14);
		}

		[Test]
		public void Should_allow_additional_roll_if_strike_in_the_last_frame()
		{
			// Arrange
			var scorer = new Scorer();

			// Act
			for (int i = 0; i < 18; i++)
			{
				scorer.Roll(2);
			}
			scorer.Roll(10);
			scorer.Roll(10);
			scorer.Roll(10);
			var statistics = scorer.GetStatistics();

			// Assert
			statistics.Last().Total.Should().Be(30, "subsequent rolls should not be summed for the last frame");
			statistics.Last().ThirdRoll.Should().Be(10, "third roll should be in statistic when strike in the last frame");

		}
	}
}