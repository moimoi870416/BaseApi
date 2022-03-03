using System;
using System.Collections.Generic;
using System.Net;
using sg.com.titansoft;

namespace Base.Api.Helper
{
    public class UrlHelper
    {
        private static string _env
        {
            get
            {
                var result = TiApplicationManager.GetGlobalSetting("Environment").ToLower();
                if (Is93SportEnable && result == "production") return "ufo";
                return result;
            }
        }
        private static bool Is93SportEnable => TiApplicationManager.GetGlobalSetting("Is93SportEnable").ToUpper() == "Y";
        private static string OldSystemInternalDomain => TiApplicationManager.GetGlobalSetting("OldSystemInternalDomain");

        private static readonly Dictionary<string, string> _port = new Dictionary<string, string>()
        {
            {"sports", "10103"},
            {"lobby", "10105"},
            {"rng", "10108"},
            {"wap", "10110"},
            {"virtualsports", "10111"},
            {"gp", "10113"},
            {"lv", "10100"}
        };

        private static string GetInternalDomainUrl(string url)
        {
            var requestUrl = new Uri(url);
            var projectPrefix = requestUrl.Host.Split('.')[0].Split('-')[0].ToLower();
            var environmentUrl =
                projectPrefix == "sports" && _env == "production" ? "wl-a141.tw01.ppanggu.com" : OldSystemInternalDomain;

            return environmentUrl + ":" + _port[projectPrefix];
        }

        public static bool ValidateUrlStatusIsAvailable(string url)
        {
            try
            {
                var urlString = "http://" + GetInternalDomainUrl(url);
                var req = (HttpWebRequest)WebRequest.Create(urlString);
                var resp = (HttpWebResponse)req.GetResponse();
                return resp.StatusCode == HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static string GetNotPrefixDomain(string url)
        {
            try
            {
                return new UriBuilder(url).Uri.Host;
            }
            catch (ArgumentNullException exception)
            {
                return "";
            }

        }

    }
}
