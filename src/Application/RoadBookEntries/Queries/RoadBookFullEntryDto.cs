using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CarsManager.Application.Common.Mappings;
using CarsManager.Domain.Entities;

namespace CarsManager.Application.RoadBookEntries.Queries
{
    public class RoadBookFullEntryDto : RoadBookBasicEntryDto, IMapFrom<RoadBookEntry>
    {
        public string MakeAndModel { get; set; }
        public string Color { get; set; }
        public List<string> Employees { get; set; } = new List<string>();
        public bool IsFullyCheckedIn { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<RoadBookEntry, RoadBookFullEntryDto>()
                .ForMember(d => d.MakeAndModel, opt => opt.MapFrom(s => $"{s.Vehicle.Model.Make.Name} {s.Vehicle.Model.Name}"))
                .ForMember(d => d.Color, opt => opt.MapFrom(s => s.Vehicle.Color))
                .ForMember(d => d.Employees, opt => opt.MapFrom(s => s.Employees.Select(e => $"{e.GivenName} {e.Surname}")))
                .ForMember(d => d.IsFullyCheckedIn, opt => opt.MapFrom(s => s.ActiveUsers.Count == 0));
        }
    }
}
