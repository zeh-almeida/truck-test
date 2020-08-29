namespace TrucksTest.API.Domain.Trucks.Validations
{
    internal static class ErrorCodes
    {
        #region Basic Info
        /// <summary>
        /// Error code for <see cref="Models.Input.UpdateTruckInput"/> <see cref="Models.Input.UpdateTruckInput.TruckType"/> validations
        /// </summary>
        public const string TruckType = "TruckError-001";

        /// <summary>
        /// Error code for <see cref="Models.Input.UpdateTruckInput"/> <see cref="Models.Input.UpdateTruckInput.FabricationYear"/> validations
        /// </summary>
        public const string FabricationYear = "TruckError-002";

        /// <summary>
        /// Error code for <see cref="Models.Input.UpdateTruckInput"/> <see cref="Models.Input.UpdateTruckInput.ModelYear"/> validations
        /// </summary>
        public const string ModelYear = "TruckError-003";
        #endregion
    }
}
