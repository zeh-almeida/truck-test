using TrucksTest.API.Domain.Trucks.Models.Entities;

namespace TrucksTest.API.Domain.Trucks.Models.Output
{
    public sealed class GetTruckResult
    {
        #region Properties
        public string Id { get; set; }

        public TruckType TruckType { get; set; }

        public int FabricationYear { get; set; }

        public int ModelYear { get; set; }

        public string Name { get; set; }

        public string Plate { get; set; }
        #endregion
    }
}
