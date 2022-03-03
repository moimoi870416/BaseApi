using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sg.com.titansoft.TiUtil.Debug;

namespace Base.Api.Helper
{
	public static class BooleanHelper
	{
		public static bool ToBoolean(this string value)
		{
			try
			{
				return bool.TryParse(value, out bool result) && result;
			}
			catch (Exception e)
			{
				TiDebugHelper.Error($"BooleanHelper.cs/ToBoolean() - Parse: {value}, To Boolean ParseError.");
				throw;
			}
		}

		public static bool IsYes(this string value)
		{
			try
			{
				return value.ToUpper()=="Y";
			}
			catch (Exception e)
			{
				TiDebugHelper.Error($"BooleanHelper.cs/IsYes() - Parse: {value}, To Boolean ParseError.");
				throw;
			}
		}
	}
}
