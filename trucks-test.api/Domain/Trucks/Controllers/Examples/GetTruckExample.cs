using Swashbuckle.AspNetCore.Filters;
using System;
using TrucksTest.API.Domain.Trucks.Models.Entities;
using TrucksTest.API.Domain.Trucks.Models.Output;

namespace TrucksTest.API.Domain.Trucks.Controllers.Examples
{
    public sealed class GetTruckExample : IExamplesProvider<GetTruckResult>
    {
        public GetTruckResult GetExamples()
        {
            return new GetTruckResult
            {
                Id = Guid.NewGuid().ToString(),
                TruckType = TruckType.FH,
                ModelYear = DateTime.Now.Year,
                FabricationYear = DateTime.Now.Year,
                Name = "Truck",
                Plate = "AAA0000"
            };
        }
    }
}
