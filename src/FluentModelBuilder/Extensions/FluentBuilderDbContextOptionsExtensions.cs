using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;

namespace FluentModelBuilder.Extensions
{
    public static class FluentBuilderDbContextOptionsExtensions
    {
        public static FluentDbContextOptionsBuilder ConfigureModel(this DbContextOptionsBuilder builder)
        {
            var extension = GetOrCreateExtension(builder);
            ((IDbContextOptionsBuilderInfrastructure)builder).AddOrUpdateExtension(extension);
            return new FluentDbContextOptionsBuilder(builder);
        }

        private static FluentModelBuilderExtension GetOrCreateExtension(DbContextOptionsBuilder builder)
        {
            var existing = builder.Options.FindExtension<FluentModelBuilderExtension>();
            return existing != null ? new FluentModelBuilderExtension(existing) : new FluentModelBuilderExtension();
        }
    }
}