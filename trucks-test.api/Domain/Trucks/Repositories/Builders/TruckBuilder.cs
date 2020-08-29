using Microsoft.EntityFrameworkCore;
using System;
using TrucksTest.API.Domain.Commons.Repositories.Builders;
using TrucksTest.API.Domain.Trucks.Models.Entities;

namespace TrucksTest.API.Domain.Trucks.Repositories.Builders
{
    public static class TruckBuilder
    {
        #region Constants
        private const string SchemaName = "TruckTest";
        #endregion

        public static ModelBuilder BuildTruck(this ModelBuilder modelBuilder)
        {
            var builder = BaseModelBuilder.Build<Truck>(modelBuilder, SchemaName);

            builder.HasKey(m => new { m.Id });

            builder.Property(p => p.Id)
                   .HasMaxLength(36)
                   .HasConversion<Guid>()
                   .IsRequired();

            builder.Property(p => p.TruckType)
                   .IsRequired();

            builder.Property(p => p.ModelYear)
                   .IsRequired();

            builder.Property(p => p.FabricationYear)
                   .IsRequired();

            builder.Property(p => p.Name)
                   .HasMaxLength(255);

            builder.Property(p => p.Plate)
                   .HasMaxLength(10);

            return modelBuilder;
        }
    }
}
