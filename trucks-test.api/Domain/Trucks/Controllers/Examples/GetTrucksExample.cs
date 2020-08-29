using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using TrucksTest.API.Domain.Trucks.Models.Entities;
using TrucksTest.API.Domain.Trucks.Models.Output;

namespace TrucksTest.API.Domain.Trucks.Controllers.Examples
{
    public sealed class GetTrucksExample : IExamplesProvider<IEnumerable<GetTruckResult>>
    {
        public IEnumerable<GetTruckResult> GetExamples()
        {
            return new List<GetTruckResult>() {
                new GetTruckResult{
                    Id = Guid.NewGuid().ToString(),
                    TruckType = TruckType.FH,
                    ModelYear = DateTime.Now.Year,
                    FabricationYear = DateTime.Now.Year,
                    Name = "Truck",
                    Plate = "AAA0000"
                }
            };
        }
    }
}
