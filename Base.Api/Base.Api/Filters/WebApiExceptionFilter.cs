using Base.Api.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using sg.com.titansoft.TiUtil.Debug;
using System.Net.Http;
using System.Text;
using System.Web.Http.Filters;

namespace Base.Api.Filters
{
    public class WebApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            TiDebugHelper.Error(
                $"{context.ActionContext.ControllerContext.ControllerDescriptor.ControllerName}Controller message: {context.Exception.Message}, callstack:{context.Exception.GetBaseException().StackTrace}");
            context.Response = new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(new
                {
                    ErrorCode = (int)ApiReturnError.GeneralError,
                    ErrorMessage = context.Exception.Message
                }, new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver()
                }), Encoding.UTF8, "application/json")
            };
        }
    }
}
