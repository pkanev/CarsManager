using System.Threading.Tasks;

namespace Client.Core.Rest
{
    class ResultStruct<T> where T : struct { }
    class RequireClass<T> where T : class { }
    public interface IApiService
    {
        Task<ApiServiceResponse<TResult>> GetAsync<TResult>(string url);
        Task<ApiServiceResponse<TResult>> PostAsync<TResult>(string url, object data = null);
        Task<ApiServiceResponse<TResult>> PutAsync<TResult>(string url, object data = null);
        Task<ApiServiceResponse<TResult>> PatchAsync<TResult>(string url, object data = null);
        Task<ApiServiceResponse<TResult>> DeleteAsync<TResult>(string url, object data = null);
        Task<ApiServiceResponse<string>> UploadAsync(string filepath);
        Task<ApiServiceResponse<string>> DeleteFileAsync(string filename);
    }
}
