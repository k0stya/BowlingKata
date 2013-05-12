namespace BowlingScorer
{
	public interface IScorer
	{
		/// <summary>
		/// Calculates current game score.
		/// </summary>
		/// <remarks>
		/// If game is not completed all remaining rolls should be treated as 0 balls.
		/// </remarks>
		/// <returns>Current game score.</returns>
		int CalculateScore();
		/// <summary>
		/// Knocks down specified number of pins in the current frame.
		/// </summary>
		/// <param name="pins">Number of pins to knock down.</param>
		void Roll(int pins);
		int?[] GetStatistics();
	}
}