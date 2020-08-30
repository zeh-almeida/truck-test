using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using TrucksTest.API.Domain.Trucks.Models.Entities;

namespace TrucksTest.API.Domain.Trucks.Repositories
{
    public interface ITruckRepository
    {
        IQueryable<Truck> Trucks();

        EntityEntry<Truck> UpdateTruck(Truck newData);

        EntityEntry<Truck> RemoveTruck(Truck data);

        EntityEntry<Truck> AddTruck(Truck data);

        void Save();
    }
}
