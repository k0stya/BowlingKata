using System;
using System.Linq;

namespace BowlingScorer
{
	public class Scorer : IScorer
	{
		private int _currentRoll;
		private readonly int?[] _statistics = new int?[21];
		private const int TOTAL_NUMBER_OF_PINS = 10;
		private const int MAX_NUMBER_OF_ROLLS = 20;

		public int CalculateScore()
		{
			int sum = 0;
			for (int i = 0; i < _currentRoll; i++)
			{
				if (IsStrike(currentRoll: i) && !IsLastFrame(currentRoll: i))
				{
					sum += _statistics.Skip(i).Where(s => s.HasValue).Take(3).Sum(s => s.Value);
					i++;
				}
				else if (IsSpare(currentRoll: i) && !IsLastFrame(currentRoll: i))
					sum += _statistics.Skip(i).Where(s => s.HasValue).Take(2).Sum(s => s.Value);
				else
					sum += _statistics[i].Value;
			}
			return sum;
		}

		private bool IsLastFrame(int currentRoll)
		{
			return currentRoll >= MAX_NUMBER_OF_ROLLS - 2;
		}

		private bool IsStrike(int currentRoll)
		{
			return _statistics[currentRoll] == TOTAL_NUMBER_OF_PINS;
		}

		private bool IsSpare(int currentRoll)
		{
			return currentRoll > 0 && _statistics[currentRoll] + _statistics[currentRoll - 1] == TOTAL_NUMBER_OF_PINS;
		}

		public void Roll(int pins)
		{
			if(pins < 0)
				throw new ArgumentException("Number of pins cannot be negative");

			if(pins > TOTAL_NUMBER_OF_PINS)
				throw new ArgumentException("Max number of available pins is 10");

			if (_currentRoll > MAX_NUMBER_OF_ROLLS - 1 + (IsAdditionalRoll(_currentRoll) ? 1 : 0))
				throw new MaxNumberOfRollsExceededException();

			_statistics[_currentRoll] = pins;

			if (IsStrike(_currentRoll) && !IsLastFrame(_currentRoll))
				_currentRoll++;

			_currentRoll++;
		}

		public int?[] GetStatistics()
		{
			return _statistics;
		}

		private bool IsAdditionalRoll(int currentRoll)
		{
			return IsLastFrame(currentRoll) && (IsSpare(currentRoll - 1) || IsStrike(currentRoll - 2));
		}
	}
}