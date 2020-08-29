using Autofac;
using TrucksTest.API.Domain.Trucks;

namespace TrucksTest.API.Domain
{
    public sealed class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterModule<TrucksModule>();
        }
    }
}
