using Newtonsoft.Json;
using sg.com.titansoft;
using sg.com.titansoft.TiUtil.Debug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Base.Api.Model;

namespace Base.Api.Filters
{
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = true)]
    public class RequestLogFilter : ActionFilterAttribute
    {       
        public override void OnActionExecuting(HttpActionContext context)
        {
            var hashCode = RandomString(8);

			try
            {
	            LogHelper.LogRequest(context, hashCode);
                context.ActionArguments.Add("HashCode", hashCode);
			}
            catch (Exception)
            {
	            // ignored
            }
        }

        public override async Task OnActionExecutedAsync(HttpActionExecutedContext context, CancellationToken cancellationToken)
        {
            try
            {
	            await LogHelper.LogResponse(context);
            }
            catch (Exception)
            {
	            // ignored
            }

            await base.OnActionExecutedAsync(context, cancellationToken).ConfigureAwait(false);
        }
        public static string RandomString(int length)
        {
	        var random = new Random(Guid.NewGuid().GetHashCode());
	        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
	        return new string(Enumerable.Repeat(chars, length)
		        .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }

    internal static class LogHelper{
        private const string AirFunHeader = "x-af-signature";
	    internal enum LogStrategy
        {
            LogAll,
            LogOnlyErrorResponse
        }

	    internal static LogStrategy GetLogStrategy(Uri requestUri)
	    {
		    var isDebugMode = TiApplicationManager.GetGlobalSetting("IsDebugMode").ToUpper() == "Y";
		    var path = requestUri.AbsolutePath;
		    var logStrategyMapping = new Dictionary<string, LogStrategy>()
		    {
			    {"/api/GameProvider/login", LogStrategy.LogOnlyErrorResponse},
                {"/Airfun/game/list", LogStrategy.LogOnlyErrorResponse},
                {"/api/back-office/transaction/get-pending-transactions", LogStrategy.LogOnlyErrorResponse},
                {"/api/common/record-application-error", LogStrategy.LogOnlyErrorResponse},
            };

		    return !isDebugMode && logStrategyMapping.ContainsKey(path) ? logStrategyMapping[path] : LogStrategy.LogAll;
	    }

	    internal static void LogRequest(HttpActionContext context, string hashcode)
	    {
		    if (GetLogStrategy(context.Request.RequestUri) == LogStrategy.LogOnlyErrorResponse) return;
            if (context.Request.Headers.TryGetValues(AirFunHeader, out var headers))
            {
                var header = headers.FirstOrDefault();
                TiDebugHelper.Debug($"[#{hashcode}] Request url => {context.Request.RequestUri.PathAndQuery}, Header => {header}, Body => {GetRequestBody(context.ActionArguments)}");
            }
            else
            {
                TiDebugHelper.Debug($"[#{hashcode}] Request url => {context.Request.RequestUri.PathAndQuery} => {GetRequestBody(context.ActionArguments)}");
            }
        }

        internal static async Task LogResponse(HttpActionExecutedContext context)
	    {
            if (context.Exception != null)
		    {
			    if (context.Exception.Message.Contains("LoginByToken"))
			    {
				    TiDebugHelper.Error($"message:{context.Exception.Message}");
			    }
			    else
			    {
				    TiDebugHelper.Error($"Response url: {context.Request.RequestUri.PathAndQuery}, message:{context.Exception.Message}, callstack: {context.Exception.StackTrace}");
			    }
		    }
		    var message = await context.Response.Content.ReadAsStringAsync();
		    message = HandleSubStringForDebugMode(message);
		    if (GetLogStrategy(context.Request.RequestUri) == LogStrategy.LogOnlyErrorResponse)
		    {
			    if (JsonConvert.DeserializeObject<BaseResponse>(message).ErrorCode != 0)
			    {
				    var request = context.ActionContext.ActionArguments;
				    TiDebugHelper.Debug($"[#{context.ActionContext.ActionArguments["HashCode"]}] Response url =>{context.Request.RequestUri.PathAndQuery} => {message} , with Request => {GetRequestBody(request)}");
			    }
			}
		    else
		    {
			    TiDebugHelper.Debug($"[#{context.ActionContext.ActionArguments["HashCode"]}] Response url => {context.Request.RequestUri.PathAndQuery} => {message}");
            }
        }

        private static string HandleSubStringForDebugMode(string message)
        {
	        var isDebugMode = TiApplicationManager.GetGlobalSetting("IsDebugMode").ToUpper() != "N";
	        var debugResponseLength = int.Parse(TiApplicationManager.GetGlobalSetting("DebugResponseLength"));
	        message = isDebugMode ? message : message.Substring(0, message.Length > debugResponseLength ? debugResponseLength : message.Length);
	        return message;
        }

        private static string GetRequestBody(Dictionary<string, object> contextActionArguments)
        {
	        return contextActionArguments.Aggregate(new StringBuilder(),
		        (sb, kvp) => sb.AppendFormat("{0}{1} = {2}", sb.Length > 0 ? ", " : "", kvp.Key, JsonConvert.SerializeObject(kvp.Value)),
		        sb => sb.ToString());
        }
    }
}