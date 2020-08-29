using Microsoft.EntityFrameworkCore;
using TrucksTest.API.Domain.Trucks.Models.Entities;
using TrucksTest.API.Domain.Trucks.Repositories.Builders;

namespace TrucksTest.API.Domain.Commons.Repositories.Context
{
    public sealed class DataContext : DbContext
    {
        #region Trucks
        public DbSet<Truck> Trucks { get; set; }
        #endregion

        #region Constructors
        public DataContext(DbContextOptions<DataContext> options)
           : base(options)
        {
        }
        #endregion

        #region Overloads
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.BuildTruck();
        }
        #endregion
    }
}
