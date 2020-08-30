using Autofac;
using AutoMapper;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TrucksTest.API.Domain.Commons.Repositories.Context;
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

            using var context = this.BuildContext();
            context.Trucks.AddRange(resultData);
            context.SaveChanges();

            var subject = this.BuildService(context);
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
            using var context = this.BuildContext();
            var subject = this.BuildService(context);

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

            using var context = this.BuildContext();
            context.Trucks.Add(model);
            context.SaveChanges();

            var subject = this.BuildService(context);
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
            using var context = this.BuildContext();
            var subject = this.BuildService(context);

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

            var input = new UpdateTruckInput
            {
                TruckType = TruckType.FM,
                ModelYear = DateTime.Now.Year,
                FabricationYear = DateTime.Now.Year,
                Name = "Truck0",
                Plate = "AAA0001"
            };

            using var context = this.BuildContext();
            var entity = context.Add(model);
            context.SaveChanges();
            entity.State = EntityState.Detached;

            var subject = this.BuildService(context);
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
            using var context = this.BuildContext();
            var subject = this.BuildService(context);

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

            using var context = this.BuildContext();
            context.Trucks.Add(model);
            context.SaveChanges();

            var subject = this.BuildService(context);
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

            using var context = this.BuildContext();
            context.Trucks.Add(model);
            context.SaveChanges();

            var subject = this.BuildService(context);

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

            using var context = this.BuildContext();
            context.Trucks.Add(model);
            context.SaveChanges();

            var subject = this.BuildService(context);

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

            using var context = this.BuildContext();
            context.Trucks.Add(model);
            context.SaveChanges();

            var subject = this.BuildService(context);

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

            using var context = this.BuildContext();
            context.Trucks.Add(model);
            context.SaveChanges();

            var subject = this.BuildService(context);

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

            using var context = this.BuildContext();
            context.Trucks.Add(model);
            context.SaveChanges();

            var subject = this.BuildService(context);
            var response = subject.Delete(model.Id.ToString());

            Assert.NotNull(response);
            Assert.Equal(model.Id.ToString(), response.Id);
        }

        [Fact]
        public void Delete_NonExistingModel_Null()
        {
            using var context = this.BuildContext();
            var subject = this.BuildService(context);

            var response = subject.Delete(Guid.NewGuid().ToString());

            Assert.Null(response);
        }

        [Fact]
        public void Delete_WithoutId_Null()
        {
            using var context = this.BuildContext();
            var subject = this.BuildService(context);

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

            using var context = this.BuildContext();
            var subject = this.BuildService(context);

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

            using var context = this.BuildContext();
            var subject = this.BuildService(context);

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

            using var context = this.BuildContext();
            var subject = this.BuildService(context);

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

            using var context = this.BuildContext();
            var subject = this.BuildService(context);

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

        private DataContext BuildContext()
        {
            var builder = new DbContextOptionsBuilder<DataContext>();

            builder
                .UseInMemoryDatabase($"{nameof(TruckServiceTest)}_{Guid.NewGuid()}")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            return new DataContext(builder.Options);
        }

        private TruckService BuildService(DataContext context)
        {
            var repository = new TruckRepository(context);

            var mapper = this.Container.Resolve<IMapper>();
            var updateValidator = this.Container.Resolve<IValidator<UpdateTruckInput>>();
            var createValidator = this.Container.Resolve<IValidator<CreateTruckInput>>();

            return new TruckService(repository, mapper, updateValidator, createValidator);
        }
    }
}
