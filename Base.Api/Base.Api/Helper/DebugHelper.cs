using Base.Api.Enums;
using Newtonsoft.Json;
using sg.com.titansoft.TiUtil.Debug;

namespace Base.Api.Helper
{
    public static class DebugHelper
    {
        public static void WriteStoredProcedureErrorLogById(string storedProcedureId, int errorCode, string errorMessage)
        {
            TiDebugHelper.Error($"StoredProcedureId:'{storedProcedureId}', ErrorCode:'{errorCode}', ErrorMessage:'{errorMessage}'");
        }

        public static void WriteStoredProcedureErrorLogByName(string storedProcedureName, int errorCode, string errorMessage)
        {
            TiDebugHelper.Error($"StoredProcedure:'{storedProcedureName}', ErrorCode:'{errorCode}', ErrorMessage:'{errorMessage}'");
        }

        public static void WriteExceptionInRepository(string storedProcedureName, int errorCode, string errorMessage)
        {
            TiDebugHelper.Error($"Exception when executing StoredProcedure:'{storedProcedureName}', ErrorCode:'{errorCode}', ErrorMessage:'{errorMessage}'");
        }

        public static void WriteApiParameterValidationErrorLog<T>(string apiUrl, ApiReturnError errorCode, T request)
        {
            TiDebugHelper.Error($"API:'{apiUrl}', ErrorType:{errorCode.ToString()}, Request:{JsonConvert.SerializeObject(request)}");
        }

        public static void WriteApiParameterValidationErrorLog<T>(string apiUrl, ApiReturnError errorCode, object error, T request)
        {
	        TiDebugHelper.Error($"API:'{apiUrl}', ErrorType:{errorCode.ToString()}, Error:{JsonConvert.SerializeObject(error)} ,Request:{JsonConvert.SerializeObject(request)}");
        }

        public static void WriteServiceErrorLog(string serviceName, string methodName, string errorMessage)
        {
            TiDebugHelper.Error($"Service: {serviceName}/{methodName}(), ErrorMessage: {errorMessage}");
        }

        public static void WriteCacheIsReloadedLog(string cacheFileName)
        {
	        TiDebugHelper.Debug($": {cacheFileName} is reloaded.");
        }
    }
}