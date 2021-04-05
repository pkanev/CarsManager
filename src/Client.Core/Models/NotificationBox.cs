using System;

namespace Client.Core.Models
{
    public class NotificationBox
    {
        public Action Callback { get; set; }
        public string Message { get; set; }
        public string Caption { get; set; }
        public bool IsPrompt { get; set; }
    }
}
