using System;
using System.Text;
using System.Collections.Generic;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using LAPhil.Application;
using LAPhil.Logging;
using LAPhil.Connectivity;



namespace LAPhil.HTTP
{
    public class HttpResponse
    {
        public HttpResponseMessage Response { get; private set; }
        public HttpResponse(HttpResponseMessage response)
        {
            Response = response;
        }

        public async Task<string> StringAsync() => await Response.Content.ReadAsStringAsync();
        public async Task<byte[]> ByteArrayAsync() => await Response.Content.ReadAsByteArrayAsync();
        public async Task<T> JsonAsync<T>() => JsonConvert.DeserializeObject<T>(await StringAsync());
    }

    public class HttpService
    {
        // https://docs.microsoft.com/en-us/azure/architecture/antipatterns/improper-instantiation/
        private readonly HttpClient HttpClient;
        Logger<HttpService> Log = ServiceContainer.Resolve<LoggingService>().GetLogger<HttpService>();
		ConnectivityService ConnectivityService = ServiceContainer.Resolve<ConnectivityService>();

        public double Timeout { get; private set; }

        public HttpService(double timeout = 6.05)
        {
            HttpClient = new HttpClient();
            HttpClient.Timeout = TimeSpan.FromSeconds(timeout);

        }

        public async Task<Result<T>> GetResultAsync<T>(string url, Dictionary<string, string> parameters = null, Dictionary<string, string> headers = null)
        {
            try
            {
                var response = await GetAsync(url: url, parameters: parameters, headers: headers);
                return new Result<T>(await response.JsonAsync<T>());
            } 
            catch(Exception ex)
            {
                return new Result<T>(ex);
            }
        }

        public async Task<Result<T>> PostResultAsync<T>(string url, Dictionary<string, string> data = null, IDictionary json = null, Dictionary<string, string> headers = null)
        {
            try
            {
                var response = await PostAsync(url: url, data: data, json: json, headers: headers);
                return new Result<T>(await response.JsonAsync<T>());
            }
            catch(Exception ex)
            {
                return new Result<T>(ex);
            }
        }

        public async Task<Result<T>> PutResultAsync<T>(string url, Dictionary<string, string> data = null, IDictionary json = null, Dictionary<string, string> headers = null)
        {
            try
            {
                var response = await PutAsync(url: url, data: data, json: json, headers: headers);
                return new Result<T>(await response.JsonAsync<T>());
            }
            catch(Exception ex)
            {
                return new Result<T>(ex);
            }
        }

        public async Task<HttpResponse> GetAsync(string url, Dictionary<string, string> parameters = null, Dictionary<string, string> headers = null)
        {
            HttpResponseMessage response = await SendGetAsync(url: url, parameters: parameters, headers: headers);
            return new HttpResponse(response);
        }

        public async Task<HttpResponse> PostAsync(string url, Dictionary<string, string> data = null, IDictionary json = null, Dictionary<string, string> headers = null)
        {
            HttpResponseMessage response = await SendPostAsync(url: url, data: data, json: json, headers: headers);
            return new HttpResponse(response);
        }

        public async Task<HttpResponse> PutAsync(string url, Dictionary<string, string> data = null, IDictionary json = null, Dictionary<string, string> headers = null)
        {
            HttpResponseMessage response = await SendPutAsync(url: url, data: data, json: json, headers: headers);
            return new HttpResponse(response);
        }

        async Task<HttpResponseMessage> SendPutAsync(string url, Dictionary<string, string> data = null, IDictionary json = null, Dictionary<string, string> headers = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            appendHeaders(request: ref request, headers: headers);

            if (data == null && json != null)
                prepareJson(request: ref request, json: json);
            else if (data != null)
                prepareUrlEncoded(request: ref request, data: data);

            return await SendRequestAsync(request);
        }

        async Task<HttpResponseMessage> SendPostAsync(string url, Dictionary<string, string> data = null, IDictionary json = null, Dictionary<string, string> headers = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            appendHeaders(request: ref request, headers: headers);

            if (data == null && json != null)
                prepareJson(request: ref request, json: json);
            else if (data != null)
                prepareUrlEncoded(request: ref request, data: data);

            return await SendRequestAsync(request);
        }

        async Task<HttpResponseMessage> SendGetAsync(string url, Dictionary<string, string> parameters, Dictionary<string, string> headers = null)
        {
            var requestUri = await prepareUrlParameters(url, parameters);
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);


            appendHeaders(request: ref request, headers: headers);
            return await SendRequestAsync(request);
        }

        async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage request)
        {
            HttpResponseMessage result;
            var cancelTokenSource = new CancellationTokenSource();

            Log.Debug("{Method} '{RequestUri}'", request.Method, request.RequestUri);

            if (ConnectivityService.IsConnected == false)
                throw new ConnectivityException();

            try
            {
                result = await HttpClient.SendAsync(request, cancelTokenSource.Token);
            }
            catch(TaskCanceledException ex)
            {
                Log.Error(ex, "Failed to {Method} '{RequestUri}', timeout ({TotalSeconds}) exceeded.", request.Method, request.RequestUri, HttpClient.Timeout.TotalSeconds);
                throw new TimeoutException();
            }
            catch(HttpRequestException ex)
            {
                Log.Error(ex, "Failed to {Method} '{RequestUri}'", request.Method, request.RequestUri);
                throw new RequestException(ex.Message);
            }

            if (result.IsSuccessStatusCode)
            {
                Log.Debug("Successfull {Method} '{RequestUri}'", request.Method, request.RequestUri);
                return result;
            }

            Log.Error("Failed to {Method} '{RequestUri}': {StatusCode}", request.Method, request.RequestUri, result.StatusCode);
            throw new HTTPError(status: result.StatusCode);
        }

        void appendHeaders(ref HttpRequestMessage request, Dictionary<string, string> headers = null)
        {
            if (headers != null)
            {
                foreach (var kvp in headers)
                {
                    request.Headers.Add(name: kvp.Key, value: kvp.Value);
                }
            }
        }

        void prepareJson(ref HttpRequestMessage request, IDictionary json = null)
        {
            if (json != null)
            {
                var encoded = JsonConvert.SerializeObject(json);
                using (var content = new StringContent(encoded, Encoding.UTF8, "application/json"))
                {
                    request.Content = content;
                }
            }
        }

        void prepareUrlEncoded(ref HttpRequestMessage request, Dictionary<string, string> data = null)
        {
            if (data != null)
            {
                using (var content = new FormUrlEncodedContent(data))
                {
                    request.Content = content;
                }
            }
        }

        async Task<string> prepareUrlParameters(string url, Dictionary<string, string> parameters)
        {
            string requestUri;

            if (parameters != null)
            {
                using (var encodedParams = new FormUrlEncodedContent(parameters))
                {
                    var value = await encodedParams.ReadAsStringAsync();
                    requestUri = string.Format("{0}?{1}", url, value);
                }
            }
            else
            {
                requestUri = url;
            }

            return requestUri;
        }
    }
}
