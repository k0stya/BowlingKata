namespace BowlingScorer
{
	public interface IScorer
	{
		int CalculateScore();
		void Roll(int pins);
		int?[] GetStatistics();
	}
}