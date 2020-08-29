using FluentValidation;
using System;
using TrucksTest.API.Domain.Trucks.Models.Entities;
using TrucksTest.API.Domain.Trucks.Models.Input;

namespace TrucksTest.API.Domain.Trucks.Validations
{
    public sealed class UpdateTruckValidation : AbstractValidator<UpdateTruckInput>
    {
        #region Constructors
        public UpdateTruckValidation()
        {
            var currentYear = DateTime.Now.Year;

            #region Truck Type
            this.RuleFor(m => m.TruckType)
                .NotEmpty()
                .WithErrorCode(ErrorCodes.TruckType)

                .IsInEnum()
                .WithErrorCode(ErrorCodes.TruckType)

                .Must(p => p == TruckType.FH || p == TruckType.FM)
                .WithErrorCode(ErrorCodes.TruckType);
            #endregion

            #region Model Year
            this.RuleFor(m => m.ModelYear)
                .NotEmpty()
                .WithErrorCode(ErrorCodes.ModelYear)

                .NotNull()
                .WithErrorCode(ErrorCodes.ModelYear)

                .InclusiveBetween(currentYear, currentYear + 1)
                .WithErrorCode(ErrorCodes.ModelYear);
            #endregion

            #region Model Year
            this.RuleFor(m => m.FabricationYear)
                .NotEmpty()
                .WithErrorCode(ErrorCodes.FabricationYear)

                .NotNull()
                .WithErrorCode(ErrorCodes.FabricationYear)

                .Equal(currentYear)
                .WithErrorCode(ErrorCodes.FabricationYear);
            #endregion
        }
        #endregion
    }
}
