using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using TrucksTest.API.Domain.Trucks.Models.Input;
using TrucksTest.API.Domain.Trucks.Models.Output;
using TrucksTest.API.Domain.Trucks.Services;

namespace TrucksTest.API.Domain.Trucks.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public sealed class TrucksController : ControllerBase
    {
        #region Properties
        private ILogger<TrucksController> Logger { get; }

        private ITruckService TruckService { get; }
        #endregion

        #region Constructors
        public TrucksController(ILogger<TrucksController> logger, ITruckService truckService)
        {
            this.Logger = logger;
            this.TruckService = truckService;
        }
        #endregion

        #region GETs
        /// <summary>
        /// Lists all available Trucks
        /// </summary>
        /// <returns>Enumeration of existing Trucks</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult<IEnumerable<GetTruckResult>> GetTrucks()
        {
            var result = this.TruckService.GetAll();
            return result.Any() ? this.Ok(result) : this.NotFound() as ActionResult;
        }

        /// <summary>
        /// List a single Truck based on the ID
        /// </summary>
        /// <param name="id">ID of the Truck to be selected</param>
        /// <returns>Single Truck</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult<GetTruckResult> GetTruck([FromRoute] string id)
        {
            var result = this.TruckService.GetSingle(id);
            return !(result is null) ? this.Ok(result) : this.NotFound() as ActionResult;
        }
        #endregion

        #region PUT
        /// <summary>
        /// Updates an existing Truck
        /// </summary>
        /// <param name="id">ID of the Truck to be updated</param>
        /// <param name="input">Data to update the Truck with</param>
        /// <returns>Updated Truck</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public ActionResult<GetTruckResult> UpdateTruck([FromRoute] string id, [FromBody] UpdateTruckInput input)
        {
            try
            {
                var result = this.TruckService.Update(id, input);
                return !(result is null) ? this.Ok(result) : this.NotFound() as ActionResult;
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex, $"Error updating Truck: {id}");
                return this.BadRequest();
            }
        }
        #endregion

        #region DELETE
        /// <summary>
        /// Deletes an existing Truck
        /// </summary>
        /// <param name="id">ID of the Truck to be deleted</param>
        /// <returns>Deleted Truck</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public ActionResult<GetTruckResult> DeleteTruck([FromRoute] string id)
        {
            try
            {
                var result = this.TruckService.Delete(id);
                return !(result is null) ? this.Ok(result) : this.NotFound() as ActionResult;
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex, $"Error deleting Truck: {id}");
                return this.BadRequest();
            }
        }
        #endregion
    }
}
