using Client.Core.Models.Authentication;

namespace Client.Core.Services
{
    public interface ICurrentUserService
    {
        public AuthModel CurrentUser { get; set; }
    }
}
