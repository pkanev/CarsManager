using System.Collections.Generic;

namespace CarsManager.Server.Models
{
    public class AuthenticateResponseModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<string> Roles { get; set; }
        public bool IsAdmin { get; set; }
        public string Token { get; set; }

    }
}
