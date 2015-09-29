using ConventionModelBuilder.Options;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;

namespace ConventionModelBuilder
{
    public class ConventionModelBuilderExtension : IDbContextOptionsExtension
    {
        private readonly DbContextOptionsBuilder _builder;
        private readonly ConventionModelBuilder _internalBuilder;
        
        public ConventionModelBuilderExtension(DbContextOptionsBuilder builder, ConventionModelBuilderOptions options)
        {
            _builder = builder;
            _internalBuilder = new ConventionModelBuilder(options);
            ((IDbContextOptionsBuilderInfrastructure) builder).AddOrUpdateExtension(this);
            _builder.UseModel(_internalBuilder.Build());
        }

        public void ApplyServices(EntityFrameworkServicesBuilder builder)
        {
            _builder.UseModel(_internalBuilder.Build());
        }
    }
}