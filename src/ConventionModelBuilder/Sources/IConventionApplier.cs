using ConventionModelBuilder.Options;
using Microsoft.Data.Entity;

namespace ConventionModelBuilder.Sources
{
    public interface IConventionApplier
    {
        void Apply(ModelBuilder modelBuilder, ConventionModelBuilderOptions options);
    }
}