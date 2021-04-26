using System.Collections.Generic;

namespace Client.Core.Models.Authentication
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Roles { get; set; }
        public bool IsAdmin { get; set; }
    }
}
