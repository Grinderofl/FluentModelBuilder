using System.Linq;
using ConventionModelBuilder.Options;
using Microsoft.Data.Entity;

namespace ConventionModelBuilder.Sources
{
    public class DefaultConventionApplier : IConventionApplier
    {
        public void Apply(ModelBuilder modelBuilder, ConventionModelBuilderOptions options)
        {
            foreach(var convention in options.Conventions.ToList())
                convention.Apply(modelBuilder);
        }
    }
}