using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using TrucksTest.API.Domain.Commons.Repositories.Context;
using TrucksTest.API.Domain.Trucks.Models.Entities;

namespace TrucksTest.API.Domain.Trucks.Repositories
{
    public sealed class TruckRepository : ITruckRepository
    {
        #region Properties
        private DataContext Context { get; }
        #endregion

        #region Constructors
        public TruckRepository(DataContext context)
        {
            this.Context = context;
        }
        #endregion

        public IQueryable<Truck> Trucks()
        {
            return this.Context.Trucks
                       .AsNoTracking()
                       .TagWith("GetTrucks")
                       .AsQueryable();
        }

        public EntityEntry<Truck> UpdateTruck(Truck newData)
        {
            var entity = this.Context.Trucks.Update(newData);
            return entity;
        }

        public EntityEntry<Truck> RemoveTruck(Truck data)
        {
            var entity = this.Context.Trucks.Remove(data);
            return entity;
        }

        public EntityEntry<Truck> AddTruck(Truck data)
        {
            var entity = this.Context.Trucks.Add(data);
            return entity;
        }

        public void Save()
        {
            this.Context.SaveChanges();
        }
    }
}
