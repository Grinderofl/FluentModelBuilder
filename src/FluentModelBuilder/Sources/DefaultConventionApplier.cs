using System.Linq;
using FluentModelBuilder.Options;
using Microsoft.Data.Entity;

namespace FluentModelBuilder.Sources
{
    public class DefaultConventionApplier : IConventionApplier
    {
        public virtual void Apply(ModelBuilder modelBuilder, FluentModelBuilderOptions options)
        {
            foreach(var convention in options.Conventions.ToList())
                convention.Apply(modelBuilder);
        }
    }
}