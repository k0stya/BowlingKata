using System;

namespace BowlingScorer
{
	public class MaxNumberOfRollsExceededException: Exception
	{
		public override string Message
		{
			get { return "Maximum number of rolls exceeded"; }
		}
	}
}