using Base.Api.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using Base.Api.Model;

namespace Base.Api.Helper
{
	public class BetHelper
	{
		public static DataTable GetRefNos<T>(List<T> refNos)
		{
			var table = new DataTable();
			table.Columns.Add("RefNo", typeof(T));
			foreach (var refNo in refNos)
			{
				var row = table.NewRow();
				row["RefNo"] = refNo;
				table.Rows.Add(row);
			}

			return table;
		}
		public static string GetBetOption(string match, string liveScore, string option, string marketType)
		{
			marketType = marketType.ToLower();
			var liveScores = new Score(liveScore);
			var matchTeams = new MatchTeams(match);
			switch (marketType)
			{
				case "asian 1x2":
				case "first half asian 1x2":
					option = GetA1X2Option(liveScores.HomeScore,liveScores.AwayScore, matchTeams.Home, matchTeams.Away, option, marketType);
					break;
			}

			return option;
		}

		public static string GetA1X2Option(int liveHomeScore, int liveAwayScore, string homeTeam, string awayTeam, string oddsOption, string marketType)
		{
			var option = string.Empty;
			var scoreDiff = liveHomeScore - liveAwayScore;
			var absScoreDiff = Math.Abs(scoreDiff);
			oddsOption = oddsOption.ToLower();
			// if draw, it's normal 1x2, but showing the team name
			if (scoreDiff == 0)
			{
				switch (oddsOption)
				{
					case "1":
						option = $"{homeTeam} wins";
						break;

					case "2":
						option = $"{homeTeam} wins";
						break;

					case "x":
						if (marketType == "asian 1x2")
						{
							option = "Match ends in a draw";
						}
						else if (marketType == "first half asian 1x2")
						{
							option = "Ends in a draw";
						}
						break;
				}
			}
			else
			{
				// winning or losing team
				var winnerTeam = string.Empty;
				var loserTeam = string.Empty;
				if (scoreDiff > 0)
				{
					winnerTeam = homeTeam;
					loserTeam = awayTeam;
				}
				else if (scoreDiff < 0)
				{
					winnerTeam = awayTeam;
					loserTeam = homeTeam;
				}

				if (oddsOption == "x")
				{
					option = $"{winnerTeam} wins by exactly {absScoreDiff} goal(s)";
				}
				else if (absScoreDiff == 1)
				{
					if (scoreDiff > 0)
					{
						if (oddsOption == "1")
						{
							option = $"{winnerTeam} wins by {absScoreDiff + 1} goal(s) or more";
						}
						else if (oddsOption == "2")
						{
							if (marketType == "asian 1x2")
							{
								option = $"{loserTeam} wins or match ends in a draw";
							}
							else if (marketType == "first half asian 1x2")
							{
								option = $"{loserTeam} wins or ends in a draw";
							}
						}
					}
					else if (scoreDiff < 0)
					{
						if (oddsOption == "1")
						{
							if (marketType == "asian 1x2")
							{
								option = $"{loserTeam} wins or match ends in a draw";
							}
							else if (marketType == "first half asian 1x2")
							{
								option = $"{loserTeam} wins or ends in a draw";
							}
						}
						else if (oddsOption == "2")
						{
							option = $"{winnerTeam} wins by {absScoreDiff + 1} goal(s) or more";
						}
					}
				}
				else
				{
					if (scoreDiff > 0)
					{
						switch (oddsOption)
						{
							case "1":
								option = $"{winnerTeam} wins by {absScoreDiff + 1} goal(s) or more";
								break;

							case "2":
								option = $"{loserTeam} loses by _{absScoreDiff - 1}_ goal(s) or less";
								break;
						}
					}
					else if (scoreDiff < 0)
					{
						switch (oddsOption)
						{
							case "1":
								option = $"{loserTeam} loses by {absScoreDiff - 1} goal(s) or less";
								break;

							case "2":
								option = $"{winnerTeam} wins by {absScoreDiff + 1} goal(s) or more";
								break;
						}
					}
				}
			}

			return option;
		}

		public static List<string> ValidateRefNoList(List<string> refNoList, ProductType productType)
		{
			if (productType == ProductType.SBO_SEAMLESS_GAME)
			{
				var seamlessRefNoRegex = new Regex(@"\S+_\S+_\S+");
				refNoList = refNoList.Where(x => seamlessRefNoRegex.IsMatch(x)).ToList();
			}
			return refNoList;
		}
	}
}