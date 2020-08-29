using System.Collections.Generic;
using TrucksTest.API.Domain.Trucks.Models.Input;
using TrucksTest.API.Domain.Trucks.Models.Output;

namespace TrucksTest.API.Domain.Trucks.Services
{
    public interface ITruckService
    {
        IEnumerable<GetTruckResult> GetAll();

        GetTruckResult GetSingle(string id);

        GetTruckResult Update(string id, UpdateTruckInput input);

        GetTruckResult Create(CreateTruckInput input);

        GetTruckResult Delete(string id);
    }
}
