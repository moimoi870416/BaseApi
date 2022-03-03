using Base.Api.Enums;
using sg.com.titansoft.TiUtil.Debug;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Base.Api.Helper
{
	public static class EnumHelper
	{
		public static T ToEnum<T>(this string str) where T : struct
		{
			return Enum.TryParse<T>(str, true, out var enumValue)
				? enumValue
				: throw new InvalidEnumArgumentException();
		}

		public static T ToEnumWithDefault<T>(this string str, T defaultEnumData) where T : struct
		{
			return Enum.TryParse<T>(str, true, out var enumValue)
				? enumValue
				: defaultEnumData;
		}

		public static string GetEnumDescription(System.Enum value)
		{
			var fi = value.GetType().GetField(value.ToString());

			DescriptionAttribute[] attributes =
				fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

			if (attributes != null && attributes.Any())
			{
				return attributes.First().Description;
			}

			return value.ToString();
		}

		public static string Description<T>(this T source)
        {
	        var field = source.GetType().GetField(source.ToString());
			if (field == null)
			{
				return source.ToString();
			}
	        var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
	        return attributes.Length > 0 ? attributes[0].Description : source.ToString();
        }

		public static SboEnumMarketType ToSboMarketTypeEnum(this string value)
		{
			try
			{
				var marketTypeMapping = new Dictionary<string, SboEnumMarketType>
				{
					{"handicap", SboEnumMarketType.Handicap},
					{"odd/even",SboEnumMarketType.Odd_Even },
					{"over/under", SboEnumMarketType.Over_Under},
					{"correct score",SboEnumMarketType.Correct_Score },
					{"1x2",SboEnumMarketType._1X2},
					{"total goal",SboEnumMarketType.Total_Goal },
					{"first half hdp", SboEnumMarketType.First_Half_Hdp },
					{"first half 1x2",SboEnumMarketType.First_Half_1x2 },
					{"first half o/u",SboEnumMarketType.First_Half_O_U },
					{"ht/ft",SboEnumMarketType.HT_FT },
					{"money line",SboEnumMarketType.Money_Line},
					{"first half o/e",SboEnumMarketType.First_Half_O_E},
					{"first goal/last goal",SboEnumMarketType.First_Goal_Last_Goal},
					{"first half cs",SboEnumMarketType.First_Half_C_S},
					{"double chance",SboEnumMarketType.Double_Chance},
					{"asian 1x2",SboEnumMarketType.Live_Score},
					{"first half asian 1x2",SboEnumMarketType.First_Half_Live_Score},
					{"outright", SboEnumMarketType.OutRight},
					{"mix parlay", SboEnumMarketType.Multiple_Bet},
					{"first half 1x2 & o/u", SboEnumMarketType.FirstHalfOverUnder1X2},
                    {"d/c & first half o/u", SboEnumMarketType.FirstHalfOverUnderDoubleChance},
					{"1x2 & o/u", SboEnumMarketType.OverUnder1X2},
                    {"d/c & o/u", SboEnumMarketType.OverUnderDoubleChance},
                    {"first half rcs", SboEnumMarketType.FirstHalfReverseCorrectScore},
                    {"reverse correct score", SboEnumMarketType.ReverseCorrectScore},
                    {"in between", SboEnumMarketType.InBetween},
				};
				var isContain = marketTypeMapping.ContainsKey(value.ToLower());
				return isContain ? marketTypeMapping[value.ToLower()] : SboEnumMarketType.Unknown;
			}
			catch (Exception e)
			{
				TiDebugHelper.Error($"ToSboMarketTypeEnum error,on value={value}, message={e.Message}");
				throw;
			}
		}

		public static SwProductType ConvertToSboSwProductType(this ProductType productType)
		{
			switch (productType)
			{
				case ProductType.SBO_SPORTS:
					return SwProductType.SBO_SPORTS;
				case ProductType.SBO_LIVE_CASINO:
					return SwProductType.SBO_LIVE_CASINO;
				case ProductType.SBO_GAMES:
					return SwProductType.SBO_GAMES;
				case ProductType.SBO_VIRTUAL_SPORTS:
					return SwProductType.SBO_VIRTUAL_SPORTS;
				case ProductType.SBO_SEAMLESS_GAME:
					return SwProductType.SBO_SEAMLESS_GAME;
				case ProductType.THIRDPARTY_SPORTS:
					return SwProductType.SBO_SEAMLESS_GAME;
				default:
					return SwProductType.SBO_SPORTS;
			}
		}

        public static ProductType ConvertPortfolioToSboProductType(int portfolio)
        {
            switch (portfolio)
            {
                case 1:
                    return ProductType.SBO_SPORTS;
                case 3:
                    return ProductType.SBO_GAMES;
				case 5:
                    return ProductType.SBO_VIRTUAL_SPORTS;
                case 7:
                    return ProductType.SBO_LIVE_CASINO;
                case 9:
                    return ProductType.SBO_SEAMLESS_GAME;
				case 10:
                    return ProductType.SBO_LIVE_COIN;
				case 11:
                    return ProductType.THIRDPARTY_SPORTS;
                default:
                    throw new NotImplementedException();
            }
        }
	}
}