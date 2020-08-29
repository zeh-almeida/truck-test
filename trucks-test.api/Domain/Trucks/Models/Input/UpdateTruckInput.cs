using TrucksTest.API.Domain.Trucks.Models.Entities;

namespace TrucksTest.API.Domain.Trucks.Models.Input
{
    public sealed class UpdateTruckInput
    {
        public TruckType TruckType { get; set; }

        public int FabricationYear { get; set; }

        public int ModelYear { get; set; }

        public string Name { get; set; }

        public string Plate { get; set; }
    }
}
