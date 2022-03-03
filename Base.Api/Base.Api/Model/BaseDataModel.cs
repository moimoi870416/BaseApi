using System;
using Base.Api.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Base.Api.Helper;
using sg.com.titansoft.TiUtil.Threading;

namespace Base.Api.Model
{
    public class ApiResponse<T> where T : IResponse
    {
        private T _data;
        private ApiReturnError _errCode = ApiReturnError.GeneralError;

        public ApiResponse()
        {
        }

        public ApiResponse(T data)
        {
            _data = data;
            _errCode = (ApiReturnError)data.ErrorCode;
        }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public T Data
        {
            get => typeof(T) != typeof(BaseResponse) && (_data != null && (ApiReturnError)_data.ErrorCode == ApiReturnError.Success) ? _data : default(T);
            set => _data = value;
        }

        public ApiReturnError ErrorCode
        {
            get => _errCode != ApiReturnError.GeneralError ? _errCode : _data == null || (ApiReturnError)_data.ErrorCode != ApiReturnError.Success ? (ApiReturnError)_data.ErrorCode : _errCode;
            set => _errCode = value;
        }

        public string Message => ErrorCode.Description();
    }

	public class BaseResponse : IResponse
    {
        public BaseResponse()
        {
        }

        public BaseResponse(int errorCode)
        {
            ErrorCode = errorCode;
        }

        public int ErrorCode { get; set; }
		public string ErrorMessage { get; set; }
        public bool IsSuccess => ErrorCode == 0;
		public bool ShouldSerializeErrorCode()
		{
			//Will serialize property "ProductType" if SerializeSensitiveInfo == true
			return false;
		}
		public bool ShouldSerializeErrorMessage()
		{
			//Will serialize property "GameType" if SerializeSensitiveInfo == true
			return false;
		}
	}

    public interface IResponse
    {
		int ErrorCode { get; set; }
		string ErrorMessage { get; set; }
    }

    public class MultiTableResponse<T1>
    {
        [JsonIgnore] public BaseResponse Error { get; set; }
        [JsonIgnore] public IEnumerable<T1> TableOne { get; set; }
    }

    public abstract class BaseMultiTableResponse<T1> : BaseResponse
    {
        protected MultiTableResponse<T1> _resp;

        protected BaseMultiTableResponse(MultiTableResponse<T1> resp)
        {
            ErrorCode = resp?.Error.ErrorCode ?? -1;
            ErrorMessage = resp == null ? "dbError" : resp.Error.ErrorMessage;
            _resp = resp;
        }
    }

    public class MultiTableResponse<T, T1>
    {
        [JsonIgnore] public BaseResponse Error { get; set; }
        [JsonIgnore] public IEnumerable<T> TableOne { get; set; }

        [JsonIgnore] public IEnumerable<T1> TableTwo { get; set; }
    }

    public abstract class BaseMultiTableResponse<T1, T2> : BaseResponse
    {
        protected MultiTableResponse<T1, T2> _resp;

        protected BaseMultiTableResponse(MultiTableResponse<T1, T2> resp)
        {
            ErrorCode = resp?.Error.ErrorCode ?? -1;
            ErrorMessage = resp == null ? "dbError" : resp.Error.ErrorMessage;
            _resp = resp;
        }
    }

    public class MultiTableResponse<T, T1, T2>
    {
        [JsonIgnore] public BaseResponse Error { get; set; }
        [JsonIgnore] public IEnumerable<T> TableOne { get; set; }

        [JsonIgnore] public IEnumerable<T1> TableTwo { get; set; }

        [JsonIgnore] public IEnumerable<T2> TableThree { get; set; }
    }

    public abstract class BaseMultiTableResponse<T, T1, T2> : BaseResponse
    {
        protected MultiTableResponse<T, T1, T2> _resp;

        protected BaseMultiTableResponse(MultiTableResponse<T, T1, T2> resp)
        {
            ErrorCode = resp?.Error.ErrorCode ?? -1;
            ErrorMessage = resp == null ? "dbError" : resp.Error.ErrorMessage;
            _resp = resp;
        }


        protected BaseMultiTableResponse(int errorCode)
        {
            ErrorCode = errorCode;
        }
    }

    public class MultiTableResponse<T, T1, T2, T3>
    {
        [JsonIgnore] public BaseResponse Error { get; set; }
        [JsonIgnore] public IEnumerable<T> TableOne { get; set; }

        [JsonIgnore] public IEnumerable<T1> TableTwo { get; set; }

        [JsonIgnore] public IEnumerable<T2> TableThree { get; set; }
        [JsonIgnore] public IEnumerable<T3> TableFour { get; set; }
    }

    public abstract class BaseMultiTableResponse<T, T1, T2, T3> : BaseResponse
    {
        protected MultiTableResponse<T, T1, T2, T3> _resp;

        protected BaseMultiTableResponse(MultiTableResponse<T, T1, T2, T3> resp)
        {
            ErrorCode = resp?.Error.ErrorCode ?? -1;
            ErrorMessage = resp == null ? "dbError" : resp.Error.ErrorMessage;
            _resp = resp;
        }
    }

    public class MultiTableResponse<T, T1, T2, T3 ,T4>
    {
        [JsonIgnore] public BaseResponse Error { get; set; }
        [JsonIgnore] public IEnumerable<T> TableOne { get; set; }

        [JsonIgnore] public IEnumerable<T1> TableTwo { get; set; }

        [JsonIgnore] public IEnumerable<T2> TableThree { get; set; }
        [JsonIgnore] public IEnumerable<T3> TableFour { get; set; }
        [JsonIgnore] public IEnumerable<T4> TableFive { get; set; }
    }

    public abstract class BaseMultiTableResponse<T, T1, T2, T3, T4> : BaseResponse
    {
        protected MultiTableResponse<T, T1, T2, T3, T4> _resp;

        protected BaseMultiTableResponse(MultiTableResponse<T, T1, T2, T3, T4> resp)
        {
            ErrorCode = resp?.Error.ErrorCode ?? -1;
            ErrorMessage = resp == null ? "dbError" : resp.Error.ErrorMessage;
            _resp = resp;
        }
    }

    public class BaseDateRange
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate
        {
            get => _endDate;
            set => _endDate = value > StartDate ? value : StartDate;
        }
        private DateTime _endDate { get; set; }
    }

}