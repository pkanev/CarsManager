using System;
using System.Threading.Tasks;

namespace Client.Core.Rest
{
    public interface IApiService
    {
        Action OnCallStart { get; set; }
        Action OnCallEnd { get; set; }
        Task<ApiServiceResponse<TResult>> GetAsync<TResult>(string url);
        Task<ApiServiceResponse<TResult>> PostAsync<TResult>(string url, object data = null);
        Task<ApiServiceResponse<TResult>> PutAsync<TResult>(string url, object data = null);
        Task<ApiServiceResponse<TResult>> PatchAsync<TResult>(string url, object data = null);
        Task<ApiServiceResponse<TResult>> DeleteAsync<TResult>(string url, object data = null);
        Task<ApiServiceResponse<string>> UploadAsync(string filepath);
        Task<ApiServiceResponse<string>> DeleteFileAsync(string filename);
    }
}
