using Autofac;
using FluentValidation;
using TrucksTest.API.Domain.Trucks.Models.Input;
using TrucksTest.API.Domain.Trucks.Repositories;
using TrucksTest.API.Domain.Trucks.Services;
using TrucksTest.API.Domain.Trucks.Validations;

namespace TrucksTest.API.Domain.Trucks
{
    /// <summary>
    /// Registers all components necessary to have the Trucks Module working
    /// <see cref="Module"/>
    /// </summary>
    public sealed class TrucksModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<TruckRepository>()
                .As<ITruckRepository>()
                .PropertiesAutowired()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<UpdateTruckValidation>()
                .As<IValidator<UpdateTruckInput>>()
                .PropertiesAutowired()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<CreateTruckValidation>()
                .As<IValidator<CreateTruckInput>>()
                .PropertiesAutowired()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<TruckService>()
                .As<ITruckService>()
                .PropertiesAutowired()
                .InstancePerLifetimeScope();
        }
    }
}
