using System;

namespace Base.Api.Model
{
	public class MatchTeams
	{
		public MatchTeams(string match)
		{
			var teams = string.IsNullOrEmpty(match)? 
				new[] {"",""} : match.Split(new[] { " vs " }, StringSplitOptions.None).Length == 2 ? 
					match.Split(new[] { " vs " }, StringSplitOptions.None) : new[] {"",""};
			Home = teams[0];
			Away = teams[1];
		}
		public string Home { get; set; }
		public string Away { get; set; }
	}
}