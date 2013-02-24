namespace BowlingScorer
{
	public interface IRepository
	{
		void Save(int?[] statistics);
		int?[] Load();
	}
}