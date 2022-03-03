using Base.Api.Enums;

namespace Base.Api.Helper
{
    public class BetSettingHelper
    {
        public static ApiReturnError ValidateMinBet(int minBet, int maxBet)
        {
            if (minBet <= 0) return ApiReturnError.MinBetLowerThanZero;
            if (minBet != -1 && maxBet > 0 && minBet >= maxBet) return ApiReturnError.MinBetGreaterThanMaxBet;
            return ApiReturnError.Success;
        }
        public static ApiReturnError ValidateMaxPerMatch(int minBet, int maxBet, int maxPerMatch)
        {
            if (maxPerMatch == 0) return ApiReturnError.MaxPerMatchEqualtoZero;
            if (minBet != -1 && maxPerMatch != -1 && minBet >= maxPerMatch) return ApiReturnError.MinBetGreaterThanMaxPerMatch;
            if (maxBet != -1 && maxPerMatch != -1 && maxBet > maxPerMatch) return ApiReturnError.MaxBetGreaterThanMaxPerMatch;
            return ApiReturnError.Success;
        }

        public static ApiReturnError ValidateMaxBet(int minBet, int maxBet)
        {
	        if (maxBet <= 0) return ApiReturnError.MaxBetLowerThanZero;
	        if (minBet != -1 && maxBet != -1 && minBet >= maxBet) return ApiReturnError.MinBetGreaterThanMaxBet;
	        return ApiReturnError.Success;
        }

		public static ApiReturnError ValidateMaxBet(int minBet, int maxBet, int maxPerMatch)
        {
            if (maxBet <= 0) return ApiReturnError.MaxBetLowerThanZero;
            if (minBet != -1 && maxBet != -1 && minBet >= maxBet) return ApiReturnError.MinBetGreaterThanMaxBet;
            if (maxBet != -1 && maxPerMatch > 0 && maxBet > maxPerMatch)
            {
                return ApiReturnError.MaxBetGreaterThanMaxPerMatch; ;
            }
            return ApiReturnError.Success;
        }
    }
}