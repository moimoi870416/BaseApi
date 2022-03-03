using System;
using System.Collections.Generic;
using System.Data;
using Base.Api.Enums;
using sg.com.titansoft.TiUtil.Web;

namespace Base.Api.Helper
{
    public class LanguageHelper
    {
        public static string GetSboLanguage(string lang)
        {
            return lang.ToLower().Replace("_", "-");
        }

        public static string ValidateApiLanguage(string lang)
        {
            if (lang.ToLower() == "zh-cn")
                return "zh-cn";
            return "en";
        }
	}
}