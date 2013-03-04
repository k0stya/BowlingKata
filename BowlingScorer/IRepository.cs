namespace BowlingScorer
{
	public interface IRepository
	{
		void Save(Frame[] statistics);
		Frame[] Load();
	}
}