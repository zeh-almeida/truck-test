using System;

namespace TrucksTest.API.Domain.Trucks.Models.Entities
{
    public class Truck
    {
        #region Properties
        public Guid Id { get; set; }

        public TruckType TruckType { get; set; }

        public int FabricationYear { get; set; }

        public int ModelYear { get; set; }

        public string Name { get; set; }

        public string Plate { get; set; }
        #endregion

        #region Constructors
        public Truck()
        {
            this.Id = Guid.NewGuid();
        }
        #endregion
    }
}
