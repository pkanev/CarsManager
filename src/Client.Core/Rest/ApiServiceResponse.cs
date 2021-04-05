using System.Collections.Generic;
using System.Net;

namespace Client.Core.Rest
{
    public class ApiServiceResponse<T>
    {
        public T Content { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccessStatusCode { get; set; }
        public string Error { get; set; }
    }
}
