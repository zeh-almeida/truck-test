using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using TrucksTest.API.Domain.Trucks.Models.Entities;
using TrucksTest.API.Domain.Trucks.Models.Input;
using TrucksTest.API.Domain.Trucks.Models.Output;
using TrucksTest.API.Domain.Trucks.Repositories;

namespace TrucksTest.API.Domain.Trucks.Services
{
    public sealed class TruckService : ITruckService
    {
        #region Properties
        private ITruckRepository Repository { get; }

        private IMapper ModelMapper { get; }

        private IValidator<UpdateTruckInput> UpdateValidator { get; }

        private IValidator<CreateTruckInput> CreateValidator { get; }
        #endregion

        #region Constructors
        public TruckService(ITruckRepository repository,
            IMapper modelMapper,
            IValidator<UpdateTruckInput> updateValidator,
            IValidator<CreateTruckInput> createValidator)
        {
            this.Repository = repository;
            this.ModelMapper = modelMapper;
            this.UpdateValidator = updateValidator;
            this.CreateValidator = createValidator;
        }
        #endregion

        #region Retrieval
        public IEnumerable<GetTruckResult> GetAll()
        {
            var data = this.Repository.Trucks();
            return this.ModelMapper.Map<IEnumerable<GetTruckResult>>(data);
        }

        public GetTruckResult GetSingle(string id)
        {
            var data = this.GetTruck(id);
            return this.ModelMapper.Map<GetTruckResult>(data);
        }

        private Truck GetTruck(string id)
        {
            return this.Repository.Trucks().Where(t => t.Id.ToString().Equals(id)).FirstOrDefault();
        }
        #endregion

        #region Update
        public GetTruckResult Update(string id, UpdateTruckInput input)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            this.UpdateValidator.ValidateAndThrow(input);

            var data = this.GetTruck(id);

            if (data is null)
            {
                return null;
            }

            var updatedData = this.ModelMapper.Map<Truck>(input);
            updatedData.Id = data.Id;

            var updated = this.Repository.UpdateTruck(updatedData);
            this.Repository.Save();

            return this.ModelMapper.Map<GetTruckResult>(updated);
        }
        #endregion

        #region Delete
        public GetTruckResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            var data = this.GetTruck(id);

            if (data is null)
            {
                return null;
            }

            var removed = this.Repository.RemoveTruck(data);
            this.Repository.Save();

            return this.ModelMapper.Map<GetTruckResult>(removed);
        }
        #endregion

        #region Create
        public GetTruckResult Create(CreateTruckInput input)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            this.CreateValidator.ValidateAndThrow(input);
            var newData = this.ModelMapper.Map<Truck>(input);

            var created = this.Repository.AddTruck(newData);
            this.Repository.Save();

            return this.ModelMapper.Map<GetTruckResult>(created);
        }
        #endregion
    }
}
