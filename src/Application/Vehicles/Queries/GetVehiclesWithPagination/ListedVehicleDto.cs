using System.Linq;
using AutoMapper;
using CarsManager.Application.Common.Mappings;
using CarsManager.Domain.Entities;

namespace CarsManager.Application.Vehicles.Queries.GetVehiclesWithPagination
{
    public class ListedVehicleDto : IMapFrom<Vehicle>
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public string Model { get; set; }
        public int MakeId { get; set; }
        public string Make { get; set; }
        public string LicencePlate { get; set; }
        public string Color { get; set; }
        public int Mileage { get; set; }
        public int ActiveRecordEntryId { get; set; }
        public bool IsCheckedOut => ActiveRecordEntryId > 0;
        public bool IsBlocked { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Vehicle, ListedVehicleDto>()
                .ForMember(d => d.Model, opt => opt.MapFrom(s => s.Model.Name))
                .ForMember(d => d.ModelId, opt => opt.MapFrom(s => s.Model.Id))
                .ForMember(d => d.Make, opt => opt.MapFrom(s => s.Model.Make.Name))
                .ForMember(d => d.MakeId, opt => opt.MapFrom(s => s.Model.Make.Id))
                .ForMember(d => d.ActiveRecordEntryId, opt => opt.MapFrom(s => s.RoadBookEntries
                        .Where(e => e.ActiveUsers.Count > 0).Select(x => x.Id).FirstOrDefault()));
        }
    }
}
