namespace BowlingScorer
{
	public class ScoreService : IScorer
	{
		private readonly IRepository _repository;
		private readonly IScorer _scorer;

		public ScoreService(IRepository repository)
		{
			_repository = repository;
			_scorer = new Scorer();
		}

		public int CalculateScore()
		{
			return _scorer.CalculateScore();
		}

		public void Roll(int pins)
		{
			_scorer.Roll(pins);
		}

		public int?[] GetStatistics()
		{
			return _scorer.GetStatistics();
		}

		public void SaveStatistics()
		{
			_repository.Save(_scorer.GetStatistics());
		}

		public void LoadStatistics()
		{
			int?[] statistics = _repository.Load();
			for (int i = 0; i < statistics.Length; i++)
			{
				if (statistics[i].HasValue)
					_scorer.Roll(statistics[i].Value);
			}
		}
	}
}