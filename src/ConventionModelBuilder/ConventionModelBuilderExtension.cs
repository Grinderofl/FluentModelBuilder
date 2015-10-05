using ConventionModelBuilder.Options;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;

namespace ConventionModelBuilder
{
    public class ConventionModelBuilderExtension : IDbContextOptionsExtension
    {
        public ConventionModelBuilderExtension(DbContextOptionsBuilder builder, ConventionModelBuilderOptions options)
        {
            var internalBuilder = new ConventionModelBuilder(options);
            ((IDbContextOptionsBuilderInfrastructure) builder).AddOrUpdateExtension(this);
            builder.UseModel(internalBuilder.Build());
        }

        public void ApplyServices(EntityFrameworkServicesBuilder builder)
        {
        }
    }
}