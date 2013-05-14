using NUnit.Framework;

namespace BowlingScorer.Test.ScorerSpecs
{
	[TestFixture]
	public class CalculateScore_Test
	{
		/* 1.
		 * 
		 * [Test]
		public void Should_return_0_if_no_rolls()
		{
		}*/

		/* 2.
		 * 
		[Test]
		[TestCase(1, 2, 3)]
		[TestCase(4, 4, 8)]
		[TestCase(3, 0, 3)]
		public void Should_sum_two_subsequent_rolls(
			int firstRoll,
			int secondRoll,
			int total)
		{
		}*/

		/* 3.
		 * 
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
		}*/

		/* 4.
		 * 
		[Test]
		[TestCase(new[] {10 , 2, 2}, 18)]
		[TestCase(new[] {0, 0, 10 , 2, 2, 1}, 19)]
		[TestCase(new[] {0, 2, 10 , 2, 2, 1}, 21)]
		[TestCase(new[] {10 , 10 , 2, 2, 2}, 42)]
		[TestCase(new[] {0, 0, 10 , 10 , 3, 3, 1}, 46)]
		[TestCase(new[] {0, 0, 10 , 10 , 10 , 2, 2, 2}, 72)]
		public void Should_sum_two_subsequent_rolls_if_strike(int[] rolls, int total)
		{
		}*/

		/* 5.
		 * 
		[Test]
		public void Should_allow_aditional_roll_if_spare_in_the_last_frame()
		{
		}*/

		/* 6.
		 * 
		[Test]
		public void Should_allow_additional_roll_if_strike_in_the_last_frame()
		{
		}*/

		/* 7.
		 * 
		[Test]
		public void Should_allow_only_20_rolls()
		{
		}*/

		/* 8.
		 * 
		[Test]
		public void Should_allow_only_19_rolls_if_strike_is_not_in_the_last_frame()
		{
		}*/

		/* 9.
		 * 
		[Test]
		public void Should_allow_addtional_20Th_roll_if_there_are_strike_in_the_last_frame_and_one_strike_before()
		{
		} */

		/* 10.
		 * 
		[Test]
		public void Should_not_allow_to_knock_down_negative_number_of_pins()
		{
		}*/

		/* 11.
		 * 
		[Test]
		public void Should_allow_knock_down_10_pins_as_max()
		{
		} */
		
		/* 12.
		 * 
		[Test]
		public void Should_return_300_for_the_perfect_game()
		{
		}*/
	}
}