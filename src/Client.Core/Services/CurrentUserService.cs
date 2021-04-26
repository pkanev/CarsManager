using Client.Core.Models.Authentication;

namespace Client.Core.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public AuthModel CurrentUser { get; set; }
    }
}
