using System;
using System.Linq;

namespace BowlingScorer
{
	public class Scorer : IScorer
	{
		private int _currentRoll;
		private readonly int?[] _statistics = new int?[21];
		private const int MAX_NUMBER_OF_PINS = 10;
		private const int MAX_NUMBER_OF_ROLLS = 20;
		private const int NUMBER_OF_FRAMES = 10;

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
			return _statistics[currentRoll] == MAX_NUMBER_OF_PINS;
		}

		private bool IsSpare(int currentRoll)
		{
			return currentRoll > 0 && _statistics[currentRoll] + _statistics[currentRoll - 1] == MAX_NUMBER_OF_PINS && _statistics[currentRoll].HasValue;
		}

		public void Roll(int pins)
		{
			if (pins < 0)
				throw new ArgumentException("Number of pins cannot be negative");

			if (pins > MAX_NUMBER_OF_PINS)
				throw new ArgumentException("Max number of available pins is 10");

			if (_currentRoll > MAX_NUMBER_OF_ROLLS - 1 + (IsAdditionalRoll(_currentRoll) ? 1 : 0))
				throw new MaxNumberOfRollsExceededException();

			_statistics[_currentRoll] = pins;

			if (IsStrike(_currentRoll) && !IsLastFrame(_currentRoll))
				_currentRoll++;

			_currentRoll++;
		}

		public Frame[] GetStatistics()
		{
			Frame[] statistics = new Frame[NUMBER_OF_FRAMES];
			int currentRoll = 0;
			for (int i = 0; i < statistics.Length; i++)
			{
				statistics[i].FirstRoll = _statistics[currentRoll++];
				statistics[i].SecondRoll = _statistics[currentRoll];
				bool isLastFrame = IsLastFrame(currentRoll);
				if (isLastFrame && (IsSpare(currentRoll) || IsStrike(currentRoll - 1)))
				{
					statistics[statistics.Length - 1].ThirdRoll = _statistics[currentRoll];
					statistics[i].Total = statistics[statistics.Length - 1].ThirdRoll;
				}
				if (IsSpare(currentRoll) && !isLastFrame)
				{
					statistics[i].Total = statistics[i].FirstRoll + statistics[i].SecondRoll + _statistics[currentRoll + 1];
				}
				else if (IsStrike(currentRoll - 1) && !isLastFrame)
				{
					if (_statistics.Skip(currentRoll + 1).Count(s => s.HasValue) < 2)
						break;
					statistics[i].Total = statistics[i].FirstRoll +
										  _statistics.Skip(currentRoll + 1).Where(s => s.HasValue).Take(2).Sum();
				}
				else
				{
					statistics[i].Total = statistics[i].FirstRoll + statistics[i].SecondRoll + 
						(statistics[i].Total.HasValue ? statistics[i].Total : 0);
				}
				currentRoll++;
			}
			return statistics;
		}

		private bool IsAdditionalRoll(int currentRoll)
		{
			return IsLastFrame(currentRoll) && (IsSpare(currentRoll - 1) || IsStrike(currentRoll - 2));
		}
	}
}