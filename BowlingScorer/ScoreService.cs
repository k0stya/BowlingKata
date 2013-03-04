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

		public Frame[] GetStatistics()
		{
			return _scorer.GetStatistics();
		}

		public void SaveStatistics()
		{
			_repository.Save(_scorer.GetStatistics());
		}

		public void LoadStatistics()
		{
			Frame[] statistics = _repository.Load();
			for (int i = 0; i < statistics.Length; i++)
			{
				if (statistics[i].FirstRoll.HasValue)
					_scorer.Roll(statistics[i].FirstRoll.Value);
				if (statistics[i].SecondRoll.HasValue)
					_scorer.Roll(statistics[i].SecondRoll.Value);
				if (statistics[i].ThirdRoll.HasValue)
					_scorer.Roll(statistics[i].ThirdRoll.Value);
			}
		}
	}
}