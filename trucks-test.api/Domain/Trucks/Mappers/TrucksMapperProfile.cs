using AutoMapper;
using TrucksTest.API.Domain.Trucks.Models.Entities;
using TrucksTest.API.Domain.Trucks.Models.Input;
using TrucksTest.API.Domain.Trucks.Models.Output;

namespace TrucksTest.API.Domain.Trucks.Mappers
{
    public sealed class TrucksMapperProfile : Profile
    {
        public TrucksMapperProfile()
        {
            this.CreateMap<Truck, GetTruckResult>();
            this.CreateMap<UpdateTruckInput, Truck>();
            this.CreateMap<CreateTruckInput, Truck>();
        }
    }
}
