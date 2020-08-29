using System.Linq;
using TrucksTest.API.Domain.Trucks.Models.Entities;

namespace TrucksTest.API.Domain.Trucks.Repositories
{
    public interface ITruckRepository
    {
        IQueryable<Truck> Trucks();

        Truck UpdateTruck(Truck newData);

        Truck RemoveTruck(Truck data);

        Truck AddTruck(Truck data);

        void Save();
    }
}
