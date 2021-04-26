using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CarsManager.Application.Common.Constants;
using CarsManager.Application.Common.Mappings;
using CarsManager.Domain.Entities;

namespace CarsManager.Application.Users
{
    public class UserDto : IMapFrom<User>
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public IList<string> Roles { get; private set; }
        public bool IsAdmin { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDto>()
                .ForMember(d => d.Roles, opt => opt.MapFrom(s => s.Roles.Select(r => r.Name)))
                .ForMember(d => d.IsAdmin, opt => opt.MapFrom(s => s.Roles.Any(r => r.Name == RoleConstants.ADMIN)));
        }
    }
}
