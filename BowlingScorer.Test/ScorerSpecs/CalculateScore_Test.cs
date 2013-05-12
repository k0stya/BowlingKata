using System;
using FluentAssertions;
using NUnit.Framework;

namespace BowlingScorer.Test.ScorerSpecs
{
	[TestFixture]
	public class CalculateScore_Test
	{
		[Test]
		public void Should_return_0_if_no_rolls()
		{
			// Arrange
			var scorer = new Scorer();

			// Act
			int score = scorer.CalculateScore();

			// Assert
			Assert.AreEqual(0, score);
		}

		[Test]
		[TestCase(1, 2, 3)]
		[TestCase(4, 4, 8)]
		[TestCase(3, 0, 3)]
		public void Should_sum_two_subsequent_rolls(
			int firstRoll,
			int secondRoll,
			int total)
		{
			// Arrange
			var scorer = new Scorer();

			// Act
			scorer.Roll(firstRoll);
			scorer.Roll(secondRoll);
			var score = scorer.CalculateScore();

			// Assert
			Assert.AreEqual(total, score);
		}

		[Test]
		[TestCase(1, 9, 9, 28)]
		[TestCase(5, 5, 5, 20)]
		[TestCase(0, 10, 5, 20)]
		public void Should_sum_one_subsequent_roll_if_spare(
			int first,
			int second,
			int third,
			int total)
		{
			// Arrange
			var scorer = new Scorer();

			// Act
			scorer.Roll(first);
			scorer.Roll(second);
			scorer.Roll(third);
			var score = scorer.CalculateScore();

			// Assert
			score.Should().Be(total);
		}

		[Test]
		[TestCase(new[] {10 /*stike*/, 2, 2}, 18)]
		[TestCase(new[] {0, 0, 10 /*stike*/, 2, 2, 1}, 19)]
		[TestCase(new[] {0, 2, 10 /*stike*/, 2, 2, 1}, 21)]
		[TestCase(new[] {10 /*stike*/, 10 /*stike*/, 2, 2, 2}, 42)]
		[TestCase(new[] {0, 0, 10 /*stike*/, 10 /*stike*/, 3, 3, 1}, 46)]
		[TestCase(new[] {0, 0, 10 /*stike*/, 10 /*stike*/, 10 /*stike*/, 2, 2, 2}, 72)]
		public void Should_sum_two_subsequent_rolls_if_strike(int[] rolls, int total)
		{
			// Arrange
			var scorer = new Scorer();

			// Act
			foreach (int pins in rolls)
			{
				scorer.Roll(pins);
			}
			var score = scorer.CalculateScore();

			// Assert
			score.Should().Be(total);
		}

		[Test]
		public void Should_allow_aditional_roll_if_spare_in_the_last_frame()
		{
			// Arrange
			var scorer = new Scorer();

			// Act
			for (int i = 0; i < 18; i++)
			{
				scorer.Roll(0);
			}
			scorer.Roll(1);
			scorer.Roll(9);
			scorer.Roll(9);
			var score = scorer.CalculateScore();

			// Assert
			score.Should().Be(19);
		}

		[Test]
		public void Should_allow_additional_roll_if_strike_in_the_last_frame()
		{
			// Arrange
			var scorer = new Scorer();

			// Act
			for (int i = 0; i < 18; i++)
			{
				scorer.Roll(1);
			}
			scorer.Roll(10);
			scorer.Roll(1);
			scorer.Roll(1);
			var score = scorer.CalculateScore();

			// Assert
			score.Should().Be(30);
		}

		[Test]
		public void Should_allow_only_20_rolls()
		{
			// Arrange
			var scorer = new Scorer();

			// Act
			for (int i = 0; i < 20; i++)
			{
				scorer.Roll(1);
			}

			// Assert
			scorer.Invoking(s => s.Roll(1)).ShouldThrow<MaxNumberOfRollsExceededException>("sdf")
			      .And.Message.Should().Be("Maximum number of rolls exceeded", "Message should be meaningful");
		}

		[Test]
		public void Should_allow_only_19_rolls_if_strike_is_not_in_the_last_frame()
		{
			// Arrange
			var scorer = new Scorer();

			// Act
			scorer.Roll(10);
			for (int i = 0; i < 18; i++)
			{
				scorer.Roll(2);
			}

			// Assert
			scorer.Invoking(r => r.Roll(1)).ShouldThrow<MaxNumberOfRollsExceededException>();
		}

		[Test]
		public void Should_allow_addtional_20Th_roll_if_there_are_strike_in_the_last_frame_and_one_strike_before()
		{
			// Arrange
			var scorer = new Scorer();

			// Act
			scorer.Roll(10);
			for (int i = 0; i < 17; i++)
			{
				scorer.Roll(0);
			}
			scorer.Roll(10);
			scorer.Roll(0);

			// Assert
			scorer.Invoking(s => s.Roll(1)).ShouldThrow<MaxNumberOfRollsExceededException>();
		}

		[Test]
		public void Should_not_allow_to_knock_down_negative_number_of_pins()
		{
			// Arrange
			var scorer = new Scorer();

			// Act && Assert
			scorer.Invoking(s => s.Roll(-1)).ShouldThrow<ArgumentException>()
			      .And.Message.Should().Be("Number of pins cannot be negative");
		}

		[Test]
		public void Should_allow_knock_down_10_pins_as_max()
		{
			// Arrange
			var scorer = new Scorer();

			// Act && assert
			scorer.Invoking(s => s.Roll(11)).ShouldThrow<ArgumentException>()
			      .And.Message.Should().Be("Max number of available pins is 10");
		}

		[Test]
		public void Should_return_300_for_the_perfect_game()
		{
			// Arrange
			var scorer = new Scorer();

			// Act
			for (int i = 0; i < 12; i++)
			{
				scorer.Roll(10);
			}
			var score = scorer.CalculateScore();

			// Assert
			score.Should().Be(300);
		}
	}
}