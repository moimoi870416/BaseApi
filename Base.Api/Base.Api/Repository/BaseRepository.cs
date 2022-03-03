using Base.Api.Helper;
using Base.Api.Model;
using sg.com.titansoft;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using DbBase.Entity;
using DbBase.Model;
using DbBase.Repository;
using sg.com.titansoft.TiUtil.Debug;

namespace Base.Api.Repository
{
    public class BaseRepository : DapperRepositoryBase<object>
    {
        public BaseRepository(DbContext dbContext) : base(dbContext)
        {
        }
        protected void LogError<T>(T response, string spId) where T : IResponse
        {
            if (response == null)
            {
                DebugHelper.WriteStoredProcedureErrorLogById(spId, -1, "Response is null");
                return;
            }
            if (response.ErrorCode != 0) DebugHelper.WriteStoredProcedureErrorLogById(spId, response.ErrorCode, response.ErrorMessage);
        }

        protected IEnumerable<T> GetData<T>(string storedProcedureId, object param = null , DbBaseTransaction baseTransaction = null)
        {
            var storedProcedureName = TiApplicationManager.GetStoredProcedureSetting(storedProcedureId);
            IEnumerable<T> result;
            try
            {
                result = Query<T>(storedProcedureName, param, 180,baseTransaction);
            }
            catch (Exception e)
            {
                TiDebugHelper.Error($"StoredProcedure:'{storedProcedureName}', Param:{JsonConvert.SerializeObject(param)}, ErrorCode:'{e.HResult}', ErrorMessage:'{e.Message}'");
                throw;
            }
            return result;
        }

        protected MultiTableResponse<T1> GetMultiTableData<T1>(string storedProcedureId, object param = null, int? commandTimeout = null)
        {
            var storedProcedureName = TiApplicationManager.GetStoredProcedureSetting(storedProcedureId);

            DbGridReader reader = null;
            try
            {
                reader = QueryMultipleReader(storedProcedureName, param, commandTimeout);
                var error = reader.IsConsumed() ? new BaseResponse(-1) : reader.Read<BaseResponse>().FirstOrDefault();
                if (error != null || error.ErrorCode == 0)
                {
                    return new MultiTableResponse<T1>()
                    {
                        Error = error,
                        TableOne = reader.IsConsumed() ? new List<T1>() : reader.Read<T1>(),
                    };
                }
                else
                {
                    DebugHelper.WriteStoredProcedureErrorLogByName(storedProcedureName, error.ErrorCode, error.ErrorMessage);
                    return new MultiTableResponse<T1>()
                    {
                        Error = error
                    };
                }
            }
            catch (Exception e)
            {
                TiDebugHelper.Error($"StoredProcedure:'{storedProcedureName}', Param:{JsonConvert.SerializeObject(param)}, ErrorCode:'{e.HResult}', ErrorMessage:'{e.Message}'");
                throw;
            }
            finally
            {
                reader?.CloseConnection();
            }
        }

        protected MultiTableResponse<T1, T2> GetMultiTableData<T1, T2>(string storedProcedureId, object param = null, int? commandTimeout = null)
        {
            var storedProcedureName = TiApplicationManager.GetStoredProcedureSetting(storedProcedureId);
            DbGridReader reader = null;
            try
            {
                reader = QueryMultipleReader(storedProcedureName, param, commandTimeout);
                var error = reader.IsConsumed()
                    ? new BaseResponse(-1) : reader.Read<BaseResponse>().FirstOrDefault();
                if (error != null && error.ErrorCode == 0)
                {
                    return new MultiTableResponse<T1, T2>()
                    {
                        Error = error,
                        TableOne = reader.IsConsumed() ? new List<T1>() : reader.Read<T1>(),
                        TableTwo = reader.IsConsumed() ? new List<T2>() : reader.Read<T2>(),
                    };
                }
                else
                {
                    return new MultiTableResponse<T1, T2>()
                    {
                        Error = error ?? new BaseResponse() { ErrorCode = -1, ErrorMessage = "dbError" }
                    };
                }
            }
            catch (Exception e)
            {
                TiDebugHelper.Error($"StoredProcedure:'{storedProcedureName}', Param:{JsonConvert.SerializeObject(param)}, ErrorCode:'{e.HResult}', ErrorMessage:'{e.Message}'");
                throw;
            }
            finally
            {
                reader?.CloseConnection();
            }
        }

        protected MultiTableResponse<T1, T2, T3> GetMultiTableData<T1, T2, T3>(string storedProcedureId, object param = null, int? commandTimeout = null)
        {
            var storedProcedureName = TiApplicationManager.GetStoredProcedureSetting(storedProcedureId);
            DbGridReader reader = null;
            try
            {
                reader = QueryMultipleReader(storedProcedureName, param, commandTimeout);
                var error = reader.IsConsumed()
                    ? new BaseResponse(-1) : reader.Read<BaseResponse>().FirstOrDefault();
                if (error != null && error.ErrorCode == 0)
                {
                    return new MultiTableResponse<T1, T2, T3>()
                    {
                        Error = error,
                        TableOne = reader.IsConsumed() ? new List<T1>() : reader.Read<T1>(),
                        TableTwo = reader.IsConsumed() ? new List<T2>() : reader.Read<T2>(),
                        TableThree = reader.IsConsumed() ? new List<T3>() : reader.Read<T3>()
                    };
                }
                else
                {
                    return new MultiTableResponse<T1, T2, T3>()
                    {
                        Error = error ?? new BaseResponse() { ErrorCode = -1, ErrorMessage = "dbError" }
                    };
                }
            }
            catch (Exception e)
            {
                TiDebugHelper.Error($"StoredProcedure:'{storedProcedureName}', Param:{JsonConvert.SerializeObject(param)}, ErrorCode:'{e.HResult}', ErrorMessage:'{e.Message}'");
                throw;
            }
            finally
            {
                reader?.CloseConnection();
            }
        }

        protected MultiTableResponse<T1, T2, T3, T4> GetMultiTableData<T1, T2, T3, T4>(string storedProcedureId, object param = null, int? commandTimeout = null)
        {
            var storedProcedureName = TiApplicationManager.GetStoredProcedureSetting(storedProcedureId);

            DbGridReader reader = null;
            try
            {
                reader = QueryMultipleReader(storedProcedureName, param, commandTimeout);
                var error = reader.IsConsumed()
                    ? new BaseResponse(-1) : reader.Read<BaseResponse>().FirstOrDefault();
                if (error != null && error.ErrorCode == 0)
                {
                    return new MultiTableResponse<T1, T2, T3, T4>()
                    {
                        Error = error,
                        TableOne = reader.IsConsumed() ? new List<T1>() : reader.Read<T1>(),
                        TableTwo = reader.IsConsumed() ? new List<T2>() : reader.Read<T2>(),
                        TableThree = reader.IsConsumed() ? new List<T3>() : reader.Read<T3>(),
                        TableFour = reader.IsConsumed() ? new List<T4>() : reader.Read<T4>()
                    };
                }
                else
                {
                    return new MultiTableResponse<T1, T2, T3, T4>()
                    {
                        Error = error ?? new BaseResponse() { ErrorCode = -1, ErrorMessage = "dbError" }
                    };
                }
            }
            catch (Exception e)
            {
                TiDebugHelper.Error($"StoredProcedure:'{storedProcedureName}', Param:{JsonConvert.SerializeObject(param)}, ErrorCode:'{e.HResult}', ErrorMessage:'{e.Message}'");
                throw;
            }
            finally
            {
                reader?.CloseConnection();
            }
        }

        protected MultiTableResponse<T1, T2, T3, T4, T5> GetMultiTableData<T1, T2, T3, T4, T5>(string storedProcedureId, object param = null, int? commandTimeout = null)
        {
            var storedProcedureName = TiApplicationManager.GetStoredProcedureSetting(storedProcedureId);

            DbGridReader reader = null;
            try
            {
                reader = QueryMultipleReader(storedProcedureName, param, commandTimeout);
                var error = reader.IsConsumed()
                    ? new BaseResponse(-1) : reader.Read<BaseResponse>().FirstOrDefault();
                if (error != null && error.ErrorCode == 0)
                {
                    return new MultiTableResponse<T1, T2, T3, T4, T5>()
                    {
                        Error = error,
                        TableOne = reader.IsConsumed() ? new List<T1>() : reader.Read<T1>(),
                        TableTwo = reader.IsConsumed() ? new List<T2>() : reader.Read<T2>(),
                        TableThree = reader.IsConsumed() ? new List<T3>() : reader.Read<T3>(),
                        TableFour = reader.IsConsumed() ? new List<T4>() : reader.Read<T4>(),
                        TableFive = reader.IsConsumed() ? new List<T5>() : reader.Read<T5>()
                    };
                }
                else
                {
                    return new MultiTableResponse<T1, T2, T3, T4,T5>()
                    {
                        Error = error ?? new BaseResponse() { ErrorCode = -1, ErrorMessage = "dbError" }
                    };
                }
            }
            catch (Exception e)
            {
                TiDebugHelper.Error($"StoredProcedure:'{storedProcedureName}', Param:{JsonConvert.SerializeObject(param)}, ErrorCode:'{e.HResult}', ErrorMessage:'{e.Message}'");
                throw;
            }
            finally
            {
                reader?.CloseConnection();
            }
        }

    }
}