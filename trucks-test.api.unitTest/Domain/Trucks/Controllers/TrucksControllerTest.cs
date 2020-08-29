using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using TrucksTest.API.Domain.Trucks.Controllers;
using TrucksTest.API.Domain.Trucks.Models.Entities;
using TrucksTest.API.Domain.Trucks.Models.Input;
using TrucksTest.API.Domain.Trucks.Models.Output;
using TrucksTest.API.Domain.Trucks.Services;
using Xunit;

namespace TrucksTest.API.UnitTest.Domain.Trucks.Controllers
{
    public sealed class TrucksControllerTest
    {
        #region GETs
        [Fact]
        public void GetTrucks_WithData_OkResult()
        {
            var resultData = new List<GetTruckResult> {
                new GetTruckResult{
                    Id = Guid.NewGuid().ToString(),
                    TruckType = TruckType.FH,
                    ModelYear = DateTime.Now.Year,
                    FabricationYear = DateTime.Now.Year,
                    Name = "Truck",
                    Plate = "AAA0000"
                }
            };

            var mockService = new Mock<ITruckService>();
            mockService.Setup(m => m.GetAll()).Returns(resultData);

            var subject = new TrucksController(Mock.Of<ILogger<TrucksController>>(), mockService.Object);
            var response = subject.GetTrucks();

            Assert.NotNull(response);
            Assert.IsType<ActionResult<IEnumerable<GetTruckResult>>>(response);

            var result = response.Result;
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            var value = (result as OkObjectResult).Value as IEnumerable<GetTruckResult>;
            Assert.NotNull(value);
            Assert.NotEmpty(value);
        }

        [Fact]
        public void GetTrucks_EmptyData_NotFoundResult()
        {
            var mockService = new Mock<ITruckService>();
            mockService.Setup(m => m.GetAll()).Returns(new List<GetTruckResult>());

            var subject = new TrucksController(Mock.Of<ILogger<TrucksController>>(), mockService.Object);
            var response = subject.GetTrucks();

            Assert.NotNull(response);
            Assert.IsType<ActionResult<IEnumerable<GetTruckResult>>>(response);

            var result = response.Result;
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetTruck_WithData_OkResult()
        {
            var resultData = new GetTruckResult
            {
                Id = Guid.NewGuid().ToString(),
                TruckType = TruckType.FH,
                ModelYear = DateTime.Now.Year,
                FabricationYear = DateTime.Now.Year,
                Name = "Truck",
                Plate = "AAA0000"
            };

            var mockService = new Mock<ITruckService>();
            mockService.Setup(m => m.GetSingle(It.IsAny<string>())).Returns(resultData);

            var subject = new TrucksController(Mock.Of<ILogger<TrucksController>>(), mockService.Object);
            var response = subject.GetTruck(resultData.Id);

            Assert.NotNull(response);
            Assert.IsType<ActionResult<GetTruckResult>>(response);

            var result = response.Result;
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            var value = (result as OkObjectResult).Value as GetTruckResult;
            Assert.NotNull(value);
            Assert.Equal(resultData, value);
        }

        [Fact]
        public void GetTruck_EmptyData_NotFoundResult()
        {
            var mockService = new Mock<ITruckService>();
            mockService.Setup(m => m.GetSingle(It.IsAny<string>())).Returns(null as GetTruckResult);

            var subject = new TrucksController(Mock.Of<ILogger<TrucksController>>(), mockService.Object);
            var response = subject.GetTruck(string.Empty);

            Assert.NotNull(response);
            Assert.IsType<ActionResult<GetTruckResult>>(response);

            var result = response.Result;
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }
        #endregion

        #region PUT
        [Fact]
        public void UpdateTruck_ExistingData_OkResult()
        {
            var resultData = new GetTruckResult
            {
                Id = Guid.NewGuid().ToString(),
                TruckType = TruckType.FH,
                ModelYear = DateTime.Now.Year,
                FabricationYear = DateTime.Now.Year,
                Name = "Truck",
                Plate = "AAA0000"
            };

            var mockService = new Mock<ITruckService>();
            mockService.Setup(m => m.Update(It.IsAny<string>(), It.IsAny<UpdateTruckInput>())).Returns(resultData);

            var subject = new TrucksController(Mock.Of<ILogger<TrucksController>>(), mockService.Object);
            var response = subject.UpdateTruck(resultData.Id, new UpdateTruckInput());

            Assert.NotNull(response);
            Assert.IsType<ActionResult<GetTruckResult>>(response);

            var result = response.Result;
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            var value = (result as OkObjectResult).Value as GetTruckResult;
            Assert.NotNull(value);
            Assert.Equal(resultData, value);
        }

        [Fact]
        public void UpdateTruck_NonExistingData_NotFoundResult()
        {
            var mockService = new Mock<ITruckService>();
            mockService.Setup(m => m.Update(It.IsAny<string>(), It.IsAny<UpdateTruckInput>())).Returns(null as GetTruckResult);

            var subject = new TrucksController(Mock.Of<ILogger<TrucksController>>(), mockService.Object);
            var response = subject.UpdateTruck(Guid.NewGuid().ToString(), new UpdateTruckInput());

            Assert.NotNull(response);
            Assert.IsType<ActionResult<GetTruckResult>>(response);

            var result = response.Result;
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void UpdateTruck_BadData_BadRequestResult()
        {
            var mockService = new Mock<ITruckService>();
            mockService.Setup(m => m.Update(It.IsAny<string>(), It.IsAny<UpdateTruckInput>())).Throws<Exception>();

            var subject = new TrucksController(Mock.Of<ILogger<TrucksController>>(), mockService.Object);
            var response = subject.UpdateTruck(Guid.NewGuid().ToString(), new UpdateTruckInput());

            Assert.NotNull(response);
            Assert.IsType<ActionResult<GetTruckResult>>(response);

            var result = response.Result;
            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(result);
        }
        #endregion
    }
}
