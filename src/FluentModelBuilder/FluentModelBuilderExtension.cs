using FluentModelBuilder.Options;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;

namespace FluentModelBuilder
{
    public class FluentModelBuilderExtension : IDbContextOptionsExtension
    {
        public FluentModelBuilderExtension(DbContextOptionsBuilder builder, FluentModelBuilderOptions options)
        {
            var internalBuilder = new FluentModelBuilder(options);
            ((IDbContextOptionsBuilderInfrastructure) builder).AddOrUpdateExtension(this);
            builder.UseModel(internalBuilder.Build());
        }

        public void ApplyServices(EntityFrameworkServicesBuilder builder)
        {
        }
    }
}