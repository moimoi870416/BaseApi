using Base.Api.Filters;
using Newtonsoft.Json;
using sg.com.titansoft.TiUtil.Debug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using sg.com.titansoft;

namespace Base.Api.Helper
{
    public interface IHttpCallingHelper
    {
        Task<T1> PostCalling<T, T1, T2>(string address, T content, T2 request, string featureTag, bool isNeedToLog)
            where T : HttpContent
            where T1 : class;
    }

    public class HttpCallingHelper : IHttpCallingHelper
    {
        private static HttpClient _clientForSeamlessWalletCalling;
        private static HttpClient _client;
        private static DecompressionMethods _currentDecompressionMethods;

        private static void InitClient()
        {
			var newDecompressionMethods = TiApplicationManager
	            .GetGlobalSetting("ResponseCompressionMethod").ToEnumWithDefault(DecompressionMethods.None);
            if (_client == null)
            {
	            _currentDecompressionMethods = newDecompressionMethods;
                TiDebugHelper.Info($"ResponseCompressionMethod INIT: current->{_currentDecompressionMethods}, new->{newDecompressionMethods}");
	            _client = new HttpClient(new HttpClientHandler
	            {
		            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
	            });
                if (newDecompressionMethods != DecompressionMethods.None)
	            {
		            _client.DefaultRequestHeaders.AcceptEncoding.Clear();
                    _client.DefaultRequestHeaders.AcceptEncoding.Add(
						new StringWithQualityHeaderValue(newDecompressionMethods.ToString().ToLower())
					);
	            }
            }
            else
            {
	            if (_currentDecompressionMethods != newDecompressionMethods)
	            {
		            TiDebugHelper.Info($"ResponseCompressionMethod Change: current->{_currentDecompressionMethods}, new->{newDecompressionMethods}");
                    _client.DefaultRequestHeaders.AcceptEncoding.Clear();
                    _client.DefaultRequestHeaders.AcceptEncoding.Add(
	                    new StringWithQualityHeaderValue(newDecompressionMethods.ToString().ToLower())
                    );
                    _currentDecompressionMethods = newDecompressionMethods;
                    TiDebugHelper.Info($"ResponseCompressionMethod Changed: current->{_currentDecompressionMethods}, new->{newDecompressionMethods}");
                }
            }
        }

        public static async Task<T1> GetCalling<T1>(string address, Dictionary<string, string> headers = null)
            where T1 : class
        {
    
                InitClient();

            var hashCode = RequestLogFilter.RandomString(8);
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    _client.DefaultRequestHeaders.Remove(header.Key);
                    _client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            TiDebugHelper.Info($"[#{hashCode}] HttpCallingHelper [Get] : Address : {address} ");
            var result = _client.GetAsync(address).Result;
            if (result.IsSuccessStatusCode)
            {
                var responseBody = await result.Content.ReadAsStringAsync();
                TiDebugHelper.Info($"[#{hashCode}] HttpCallingHelper [Get] :  Address : {address} Data:{responseBody}");
                return JsonConvert.DeserializeObject<T1>(responseBody);
            }

            TiDebugHelper.Info(
                $"[#{hashCode}] HttpCallingHelper [Get] :  Address : {address} Response.StatusCode = {result.StatusCode}");
            return null;
        }

        public async Task<T1> PostCalling<T, T1, T2>(string address, T content, T2 request, string featureTag,
            bool isNeedToLog)
            where T : HttpContent
            where T1 : class
        {
	        InitClient();
	        var hashCode = RequestLogFilter.RandomString(8);
	        if (isNeedToLog)
	        {
		        TiDebugHelper.Info($"[#{hashCode}] HttpCallingHelper:{featureTag} Request To : Address : {address} Data:{JsonConvert.SerializeObject(request)}");
            }
            try
            {
                var result = _client.PostAsync(address, content).GetAwaiter().GetResult();
                if (result.IsSuccessStatusCode)
                {

                    var responseBody = await result.Content.ReadAsStringAsync();
                    if (isNeedToLog)
                    {
	                    TiDebugHelper.Info($"[#{hashCode}] HttpCallingHelper:{featureTag} Response From : Address : {address} Data:{responseBody}");
                    }
    
                    return JsonConvert.DeserializeObject<T1>(responseBody);
                }

                if (isNeedToLog)
                    TiDebugHelper.Info(
                        $"[#{hashCode}] HttpCallingHelper:{featureTag} Response From : Address : {address} Response.StatusCode = {result.StatusCode}");
                return null;
            }
            catch (Exception e)
            {
                TiDebugHelper.Error(
                    $"[#{hashCode}] HttpCallingHelper:{featureTag} Response From : Address : {address} ex = {e.Message}");
            }

            return null;
        }

        public static async Task<T1> PostCalling<T, T1, T2>(string address, T content, T2 request, string featureTag,
            bool isNeedToLog, string header)
            where T : HttpContent
            where T1 : class
        {
                InitClient();

            var hashCode = RequestLogFilter.RandomString(8);
            if (isNeedToLog)
                TiDebugHelper.Info(
                    $"[#{hashCode}] HttpCallingHelper:{featureTag} Request To : Address : {address} Data:{JsonConvert.SerializeObject(request)}");
            try
            {
                _client.DefaultRequestHeaders.Remove("x-af-signature");
                _client.DefaultRequestHeaders.Add("x-af-signature", header);
                var result = _client.PostAsync(address, content).GetAwaiter().GetResult();
                if (result.IsSuccessStatusCode)
                {
                    var responseBody = await result.Content.ReadAsStringAsync();
                    if (isNeedToLog)
                        TiDebugHelper.Info(
                            $"[#{hashCode}] HttpCallingHelper:{featureTag} Response From : Address : {address} Data:{responseBody}");
                    return JsonConvert.DeserializeObject<T1>(responseBody);
                }

                if (isNeedToLog)
                    TiDebugHelper.Info(
                        $"[#{hashCode}] HttpCallingHelper:{featureTag} Response From : Address : {address} Response.StatusCode = {result.StatusCode}");
                return null;
            }
            catch (Exception e)
            {
                TiDebugHelper.Error(
                    $"[#{hashCode}] HttpCallingHelper:{featureTag} Response From : Address : {address} ex = {e.Message}");
            }

            return null;
        }

        public static async Task<T1> PostCallingAsync<T, T1, T2>(string address, T content, T2 request,
            string featureTag, bool isNeedToLog)
            where T : HttpContent
            where T1 : class
        {
            if (_clientForSeamlessWalletCalling == null)
            {
                _clientForSeamlessWalletCalling = new HttpClient();
                _clientForSeamlessWalletCalling.Timeout = TimeSpan.FromSeconds(5);
            }

            var hashCode = RequestLogFilter.RandomString(8);
            if (isNeedToLog)
                TiDebugHelper.Info(
                    $"[#{hashCode}][NoGetAwate] HttpCallingHelper:{featureTag} Request To : Address : {address} Data:{JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _clientForSeamlessWalletCalling.PostAsync(address, content);
                if (result.IsSuccessStatusCode)
                {
                    var responseBody = await result.Content.ReadAsStringAsync();
                    if (isNeedToLog)
                        TiDebugHelper.Info(
                            $"[#{hashCode}][NoGetAwate] HttpCallingHelper:{featureTag} Response From : Address : {address} Data:{responseBody}");
                    return JsonConvert.DeserializeObject<T1>(responseBody);
                }

                if (isNeedToLog)
                    TiDebugHelper.Info(
                        $"[#{hashCode}][NoGetAwate] HttpCallingHelper:{featureTag} Response From : Address : {address} Response.StatusCode = {result.StatusCode}");
                return null;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    TiDebugHelper.Error(
                        $"[#{hashCode}] HttpCallingHelper:{featureTag} Response From : Address : {address} ex = {e.InnerException.Message}");
                }
                else
                {
                    TiDebugHelper.Error(
                        $"[#{hashCode}] HttpCallingHelper:{featureTag} Response From : Address : {address} ex = {e.Message}");
                }
            }

            return null;
        }
    }
}