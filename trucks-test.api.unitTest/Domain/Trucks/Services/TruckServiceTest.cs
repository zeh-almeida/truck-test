using Autofac;
using AutoMapper;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using FluentValidation;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using TrucksTest.API.Domain.Trucks;
using TrucksTest.API.Domain.Trucks.Models.Entities;
using TrucksTest.API.Domain.Trucks.Models.Input;
using TrucksTest.API.Domain.Trucks.Repositories;
using TrucksTest.API.Domain.Trucks.Services;
using Xunit;

namespace TrucksTest.API.UnitTest.Domain.Trucks.Services
{
    public sealed class TruckServiceTest
    {
        #region Properties
        private IContainer Container { get; }
        #endregion

        #region Constructors
        public TruckServiceTest()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<TrucksModule>();
            builder.AddAutoMapper(typeof(TrucksModule).Assembly);

            this.Container = builder.Build();
        }
        #endregion

        #region Retrieval
        [Fact]
        public void GetAll_WithData_ShouldMatch()
        {
            var resultData = new List<Truck> {
                new Truck{
                Id = Guid.NewGuid(),
                    TruckType = TruckType.FH,
                    ModelYear = DateTime.Now.Year,
                    FabricationYear = DateTime.Now.Year,
                    Name = "Truck",
                    Plate = "AAA0000"
                }
            };

            var mockRepository = new Mock<ITruckRepository>();
            mockRepository.Setup(m => m.Trucks()).Returns(resultData.AsQueryable());

            var subject = this.BuildService(mockRepository);
            var response = subject.GetAll();

            Assert.NotNull(response);
            Assert.NotEmpty(response);
            Assert.Equal(resultData.Count, response.Count());

            var dbItem = response.First();
            var resultItem = response.First();

            Assert.Equal(dbItem.TruckType, resultItem.TruckType);
            Assert.Equal(dbItem.ModelYear, resultItem.ModelYear);
            Assert.Equal(dbItem.FabricationYear, resultItem.FabricationYear);
            Assert.Equal(dbItem.Name, resultItem.Name);
            Assert.Equal(dbItem.Plate, resultItem.Plate);
        }

        [Fact]
        public void GetAll_WithoutData_Empty()
        {
            var mockRepository = new Mock<ITruckRepository>();
            mockRepository.Setup(m => m.Trucks()).Returns(new List<Truck>().AsQueryable());

            var subject = this.BuildService(mockRepository);
            var response = subject.GetAll();

            Assert.NotNull(response);
            Assert.Empty(response);
        }

        [Fact]
        public void GetSingle_WithData_ShouldMatch()
        {
            var model = new Truck
            {
                Id = Guid.NewGuid(),
                TruckType = TruckType.FH,
                ModelYear = DateTime.Now.Year,
                FabricationYear = DateTime.Now.Year,
                Name = "Truck",
                Plate = "AAA0000"
            };

            var resultData = new List<Truck> { model };

            var mockRepository = new Mock<ITruckRepository>();
            mockRepository.Setup(m => m.Trucks()).Returns(resultData.AsQueryable());

            var subject = this.BuildService(mockRepository);
            var response = subject.GetSingle(model.Id.ToString());

            Assert.NotNull(response);
            Assert.Equal(model.Id.ToString(), response.Id);

            Assert.Equal(model.TruckType, response.TruckType);
            Assert.Equal(model.ModelYear, response.ModelYear);
            Assert.Equal(model.FabricationYear, response.FabricationYear);
            Assert.Equal(model.Name, response.Name);
            Assert.Equal(model.Plate, response.Plate);
        }

        [Fact]
        public void GetSingle_WithoutMatch_Null()
        {
            var mockRepository = new Mock<ITruckRepository>();
            mockRepository.Setup(m => m.Trucks()).Returns(new List<Truck>().AsQueryable());

            var subject = this.BuildService(mockRepository);
            var response = subject.GetSingle(string.Empty);

            Assert.Null(response);
        }
        #endregion

        #region Update
        [Fact]
        public void Update_ExistingModel_ShouldReturn()
        {
            var model = new Truck
            {
                Id = Guid.NewGuid(),
                TruckType = TruckType.FH,
                ModelYear = DateTime.Now.Year,
                FabricationYear = DateTime.Now.Year,
                Name = "Truck",
                Plate = "AAA0000"
            };
            var resultModel = new Truck
            {
                Id = model.Id,
                TruckType = TruckType.FM,
                ModelYear = DateTime.Now.Year,
                FabricationYear = DateTime.Now.Year,
                Name = "Truck0",
                Plate = "AAA0001"
            };

            var input = new UpdateTruckInput
            {
                TruckType = TruckType.FM,
                ModelYear = DateTime.Now.Year,
                FabricationYear = DateTime.Now.Year,
                Name = "Truck0",
                Plate = "AAA0001"
            };

            var mockRepository = new Mock<ITruckRepository>();
            mockRepository.Setup(m => m.Trucks()).Returns(new List<Truck> { resultModel }.AsQueryable());
            mockRepository.Setup(m => m.UpdateTruck(It.IsAny<Truck>())).Returns(resultModel);
            mockRepository.Setup(m => m.Save()).Verifiable();

            var subject = this.BuildService(mockRepository);
            var response = subject.Update(model.Id.ToString(), input);

            Assert.NotNull(response);
            Assert.Equal(model.Id.ToString(), response.Id);

            Assert.Equal(input.TruckType, response.TruckType);
            Assert.Equal(input.ModelYear, response.ModelYear);
            Assert.Equal(input.FabricationYear, response.FabricationYear);
            Assert.Equal(input.Name, response.Name);
            Assert.Equal(input.Plate, response.Plate);
        }

        [Fact]
        public void Update_NonExistingModel_Null()
        {
            var mockRepository = new Mock<ITruckRepository>();
            mockRepository.Setup(m => m.Trucks()).Returns(new List<Truck> { }.AsQueryable());

            var subject = this.BuildService(mockRepository);
            var response = subject.Update(Guid.NewGuid().ToString(), new UpdateTruckInput
            {
                TruckType = TruckType.FM,
                ModelYear = DateTime.Now.Year,
                FabricationYear = DateTime.Now.Year,
                Name = "Truck",
                Plate = "AAA0000"
            });

            Assert.Null(response);
        }

        [Fact]
        public void Update_WithoutId_Null()
        {
            var model = new Truck
            {
                Id = Guid.NewGuid(),
                TruckType = TruckType.FH,
                ModelYear = DateTime.Now.Year,
                FabricationYear = DateTime.Now.Year,
                Name = "Truck",
                Plate = "AAA0000"
            };

            var mockRepository = new Mock<ITruckRepository>();
            mockRepository.Setup(m => m.Trucks()).Returns(new List<Truck> { model }.AsQueryable());

            var subject = this.BuildService(mockRepository);
            var response = subject.Update(null, new UpdateTruckInput());

            Assert.Null(response);
        }

        [Fact]
        public void Update_WithoutInput_Throws()
        {
            var model = new Truck
            {
                Id = Guid.NewGuid(),
                TruckType = TruckType.FH,
                ModelYear = DateTime.Now.Year,
                FabricationYear = DateTime.Now.Year,
                Name = "Truck",
                Plate = "AAA0000"
            };

            var mockRepository = new Mock<ITruckRepository>();
            mockRepository.Setup(m => m.Trucks()).Returns(new List<Truck> { model }.AsQueryable());

            var subject = this.BuildService(mockRepository);

            Assert.ThrowsAny<ArgumentNullException>(() => subject.Update(model.Id.ToString(), null));
        }

        [Theory]
        [InlineData(TruckType.FMX)]
        [InlineData(TruckType.VM)]
        public void Update_BadTruckType_Throws(TruckType truckType)
        {
            var model = new Truck
            {
                Id = Guid.NewGuid(),
                TruckType = TruckType.FH,
                ModelYear = DateTime.Now.Year,
                FabricationYear = DateTime.Now.Year,
                Name = "Truck",
                Plate = "AAA0000"
            };

            var mockRepository = new Mock<ITruckRepository>();
            mockRepository.Setup(m => m.Trucks()).Returns(new List<Truck> { model }.AsQueryable());

            var subject = this.BuildService(mockRepository);

            Assert.Throws<ValidationException>(() => subject.Update(model.Id.ToString(), new UpdateTruckInput
            {
                TruckType = truckType,
                ModelYear = DateTime.Now.Year,
                FabricationYear = DateTime.Now.Year,
                Name = "Truck",
                Plate = "AAA0000"
            }));
        }

        [Fact]
        public void Update_BadModelYear_Throws()
        {
            var model = new Truck
            {
                Id = Guid.NewGuid(),
                TruckType = TruckType.FH,
                ModelYear = DateTime.Now.Year,
                FabricationYear = DateTime.Now.Year,
                Name = "Truck",
                Plate = "AAA0000"
            };

            var mockRepository = new Mock<ITruckRepository>();
            mockRepository.Setup(m => m.Trucks()).Returns(new List<Truck> { model }.AsQueryable());

            var subject = this.BuildService(mockRepository);

            Assert.Throws<ValidationException>(() => subject.Update(model.Id.ToString(), new UpdateTruckInput
            {
                TruckType = TruckType.FH,
                ModelYear = DateTime.Now.Year - 1,
                FabricationYear = DateTime.Now.Year,
                Name = "Truck",
                Plate = "AAA0000"
            }));
        }

        [Fact]
        public void Update_BadFabricationYear_Throws()
        {
            var model = new Truck
            {
                Id = Guid.NewGuid(),
                TruckType = TruckType.FH,
                ModelYear = DateTime.Now.Year,
                FabricationYear = DateTime.Now.Year,
                Name = "Truck",
                Plate = "AAA0000"
            };

            var mockRepository = new Mock<ITruckRepository>();
            mockRepository.Setup(m => m.Trucks()).Returns(new List<Truck> { model }.AsQueryable());

            var subject = this.BuildService(mockRepository);

            Assert.Throws<ValidationException>(() => subject.Update(model.Id.ToString(), new UpdateTruckInput
            {
                TruckType = TruckType.FH,
                ModelYear = DateTime.Now.Year,
                FabricationYear = DateTime.Now.Year - 1,
                Name = "Truck",
                Plate = "AAA0000"
            }));
        }
        #endregion

        #region Delete
        [Fact]
        public void Delete_ExistingModel_ShouldReturn()
        {
            var model = new Truck
            {
                Id = Guid.NewGuid(),
                TruckType = TruckType.FH,
                ModelYear = DateTime.Now.Year,
                FabricationYear = DateTime.Now.Year,
                Name = "Truck",
                Plate = "AAA0000"
            };

            var mockRepository = new Mock<ITruckRepository>();
            mockRepository.Setup(m => m.Trucks()).Returns(new List<Truck> { model }.AsQueryable());
            mockRepository.Setup(m => m.RemoveTruck(It.IsAny<Truck>())).Returns(model);
            mockRepository.Setup(m => m.Save()).Verifiable();

            var subject = this.BuildService(mockRepository);
            var response = subject.Delete(model.Id.ToString());

            Assert.NotNull(response);
            Assert.Equal(model.Id.ToString(), response.Id);
        }

        [Fact]
        public void Delete_NonExistingModel_Null()
        {
            var mockRepository = new Mock<ITruckRepository>();
            mockRepository.Setup(m => m.Trucks()).Returns(new List<Truck> { }.AsQueryable());

            var subject = this.BuildService(mockRepository);
            var response = subject.Delete(Guid.NewGuid().ToString());

            Assert.Null(response);
        }

        [Fact]
        public void Delete_WithoutId_Null()
        {
            var model = new Truck
            {
                Id = Guid.NewGuid(),
                TruckType = TruckType.FH,
                ModelYear = DateTime.Now.Year,
                FabricationYear = DateTime.Now.Year,
                Name = "Truck",
                Plate = "AAA0000"
            };

            var mockRepository = new Mock<ITruckRepository>();
            mockRepository.Setup(m => m.Trucks()).Returns(new List<Truck> { model }.AsQueryable());

            var subject = this.BuildService(mockRepository);
            var response = subject.Delete(null);

            Assert.Null(response);
        }
        #endregion

        #region Create
        [Fact]
        public void Create_WithoutInput_Throws()
        {
            var model = new Truck
            {
                Id = Guid.NewGuid(),
                TruckType = TruckType.FH,
                ModelYear = DateTime.Now.Year,
                FabricationYear = DateTime.Now.Year,
                Name = "Truck",
                Plate = "AAA0000"
            };

            var mockRepository = new Mock<ITruckRepository>();
            mockRepository.Setup(m => m.Trucks()).Returns(new List<Truck> { model }.AsQueryable());

            var subject = this.BuildService(mockRepository);

            Assert.ThrowsAny<ArgumentNullException>(() => subject.Create(null));
        }

        [Theory]
        [InlineData(TruckType.FMX)]
        [InlineData(TruckType.VM)]
        public void Create_BadTruckType_Throws(TruckType truckType)
        {
            var model = new Truck
            {
                Id = Guid.NewGuid(),
                TruckType = TruckType.FH,
                ModelYear = DateTime.Now.Year,
                FabricationYear = DateTime.Now.Year,
                Name = "Truck",
                Plate = "AAA0000"
            };

            var mockRepository = new Mock<ITruckRepository>();
            mockRepository.Setup(m => m.Trucks()).Returns(new List<Truck> { model }.AsQueryable());

            var subject = this.BuildService(mockRepository);

            Assert.Throws<ValidationException>(() => subject.Create(new CreateTruckInput
            {
                TruckType = truckType,
                ModelYear = DateTime.Now.Year,
                FabricationYear = DateTime.Now.Year,
                Name = "Truck",
                Plate = "AAA0000"
            }));
        }

        [Fact]
        public void Create_BadModelYear_Throws()
        {
            var model = new Truck
            {
                Id = Guid.NewGuid(),
                TruckType = TruckType.FH,
                ModelYear = DateTime.Now.Year,
                FabricationYear = DateTime.Now.Year,
                Name = "Truck",
                Plate = "AAA0000"
            };

            var mockRepository = new Mock<ITruckRepository>();
            mockRepository.Setup(m => m.Trucks()).Returns(new List<Truck> { model }.AsQueryable());

            var subject = this.BuildService(mockRepository);

            Assert.Throws<ValidationException>(() => subject.Create(new CreateTruckInput
            {
                TruckType = TruckType.FH,
                ModelYear = DateTime.Now.Year - 1,
                FabricationYear = DateTime.Now.Year,
                Name = "Truck",
                Plate = "AAA0000"
            }));
        }

        [Fact]
        public void Create_BadFabricationYear_Throws()
        {
            var model = new Truck
            {
                Id = Guid.NewGuid(),
                TruckType = TruckType.FH,
                ModelYear = DateTime.Now.Year,
                FabricationYear = DateTime.Now.Year,
                Name = "Truck",
                Plate = "AAA0000"
            };

            var mockRepository = new Mock<ITruckRepository>();
            mockRepository.Setup(m => m.Trucks()).Returns(new List<Truck> { model }.AsQueryable());

            var subject = this.BuildService(mockRepository);

            Assert.Throws<ValidationException>(() => subject.Create(new CreateTruckInput
            {
                TruckType = TruckType.FH,
                ModelYear = DateTime.Now.Year,
                FabricationYear = DateTime.Now.Year - 1,
                Name = "Truck",
                Plate = "AAA0000"
            }));
        }
        #endregion

        private TruckService BuildService(Mock<ITruckRepository> repositoryMock)
        {
            var mapper = this.Container.Resolve<IMapper>();
            var updateValidator = this.Container.Resolve<IValidator<UpdateTruckInput>>();
            var createValidator = this.Container.Resolve<IValidator<CreateTruckInput>>();

            return new TruckService(repositoryMock.Object, mapper, updateValidator, createValidator);
        }
    }
}
