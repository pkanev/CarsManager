using System;
using System.Threading.Tasks;

namespace Client.Core.Models
{
    public class UploadInteractionHandler
    {
        public Action<string> Callback { get; set; }
    }

    public class UploadInteractionAsyncHandler
    {
        public Func<string, Task> Callback { get; set; }
    }
}
