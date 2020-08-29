using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TrucksTest.API.Domain.Commons.Repositories.Builders
{
    public static class BaseModelBuilder
    {
        public static EntityTypeBuilder<T> Build<T>(ModelBuilder modelBuilder, string schemaName)
            where T : class, new()
        {
            var builder = modelBuilder?.Entity<T>();
            var type = typeof(T);

            builder.ToTable(type.Name, schemaName);
            return builder;
        }
    }
}
