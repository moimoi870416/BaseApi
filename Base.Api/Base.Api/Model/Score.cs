using System;

namespace Base.Api.Model
{
	public class Score
	{
		public Score(string score)
		{
			var scores = string.IsNullOrEmpty(score) ?
				new[] { "", "" } : score.Split(new[] { ":" }, StringSplitOptions.None).Length == 2 ?
					score.Split(new[] { ":" }, StringSplitOptions.None) : new[] { "", "" };
			int.TryParse(scores[0], out var homeScore);
			int.TryParse(scores[1], out var awayScore);
			HomeScore = homeScore;
			AwayScore = awayScore;
		}
		public int HomeScore { get; set; }
		public int AwayScore { get; set; }
	}
}