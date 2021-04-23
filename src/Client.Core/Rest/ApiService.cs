using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Logging;
using Newtonsoft.Json;

namespace Client.Core.Rest
{
    public class ApiService : IApiService
    {
        private const string uploadsEndPoint = "uploads";
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IMvxLog mvxLog;
        private string baseUrl => Properties.Settings.Default.ApiAddress;

        public ApiService(IHttpClientFactory httpClientFactory, IMvxLog mvxLog)
        {
            this.httpClientFactory = httpClientFactory;
            this.mvxLog = mvxLog;
        }

        public async Task<ApiServiceResponse<TResult>> GetAsync<TResult>(string url)
            => await MakeApiCall<TResult>(url, HttpMethod.Get);

        public async Task<ApiServiceResponse<TResult>> PostAsync<TResult>(string url, object data = null)
            => await MakeApiCall<TResult>(url, HttpMethod.Post, data);

        public async Task<ApiServiceResponse<TResult>> PutAsync<TResult>(string url, object data = null)
            => await MakeApiCall<TResult>(url, HttpMethod.Put, data);
        
        public async Task<ApiServiceResponse<TResult>> PatchAsync<TResult>(string url, object data = null)
            => await MakeApiCall<TResult>(url, HttpMethod.Patch, data);

        public async Task<ApiServiceResponse<TResult>> DeleteAsync<TResult>(string url, object data = null)
            => await MakeApiCall<TResult>(url, HttpMethod.Delete, data);

        private async Task<ApiServiceResponse<TResult>> MakeApiCall<TResult>(string url, HttpMethod method, object data = null)
        {
            string targetUrl = $"{baseUrl}{url}";

            using (var httpClient = httpClientFactory.CreateClient())
            {
                using (var request = new HttpRequestMessage { RequestUri = new Uri(targetUrl), Method = method })
                {
                    if (method != HttpMethod.Get)
                    {
                        var json = JsonConvert.SerializeObject(data);
                        request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                    }

                    var result = new ApiServiceResponse<TResult>();
                    try
                    {
                        var response = await httpClient.SendAsync(request).ConfigureAwait(false);
                        result.StatusCode = response.StatusCode;
                        result.IsSuccessStatusCode = response.IsSuccessStatusCode;

                        if (response.IsSuccessStatusCode)
                        {
                            var stringSerialized = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                            result.Content = JsonConvert.DeserializeObject<TResult>(stringSerialized);
                        }
                        else
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            string errorMessage = @$"{GetStatusCodeErrorMessage(response.StatusCode)}/r/n{content}";
                            result.Error = errorMessage;
                        }

                    }
                    catch (Exception ex)
                    {
                        mvxLog.ErrorException("MakeApiCall failed", ex);
                        result.Error = ex.Message;
                    }

                    return result;
                }
            }
        }

        public async Task<ApiServiceResponse<string>> UploadAsync(string filePath)
        {
            string targetUrl = $"{baseUrl}{uploadsEndPoint}";

            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File [{filePath}] not found.");
            }

            using (var httpClient = httpClientFactory.CreateClient())
            {
                using var form = new MultipartFormDataContent();
                using var fileContent = new ByteArrayContent(await File.ReadAllBytesAsync(filePath));
                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                form.Add(fileContent, "file", Path.GetFileName(filePath));

                var result = new ApiServiceResponse<string>();

                try
                {
                    var response = await httpClient.PostAsync(targetUrl, form);

                    result.StatusCode = response.StatusCode;
                    result.IsSuccessStatusCode = response.IsSuccessStatusCode;

                    if (response.IsSuccessStatusCode)
                        result.Content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    else
                        result.Error = GetStatusCodeErrorMessage(response.StatusCode);
                }
                catch (Exception ex)
                {

                    mvxLog.ErrorException("MakeApiCall failed", ex);
                    result.Error = ex.Message;
                }

                return result;
            }
        }

        public async Task<ApiServiceResponse<string>> DeleteFileAsync(string filename)
            => await this.DeleteAsync<string>(uploadsEndPoint, new {FileName = filename});

        //TODO: NEEDS BETTER HANDLING
        private string GetStatusCodeErrorMessage(HttpStatusCode statusCode)
            => statusCode switch
            {
                HttpStatusCode.Unauthorized => "Unauthorized access, please log in to the system.",
                HttpStatusCode.NotFound => "Could not find the specified resource",
                HttpStatusCode.InternalServerError => "Sorry, something went wrong",
                _ => statusCode.ToString()
            };
    }
}
