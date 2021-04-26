using System;
using System.Threading.Tasks;

namespace Client.Core.Models
{
    public class NavigationModel
    {
        public Func<Task> Callback { get; set; }
        public Func<Task> CancelCallback { get; set; }
    }

    public class NavigationModel<T>
    {
        public T Data { get; set; }
        public Func<Task> Callback { get; set; }
        public Func<Task> CancelCallback { get; set; }
    }
}
