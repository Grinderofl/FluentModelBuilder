using FluentModelBuilder.Conventions.Entities.Options;
using Microsoft.Data.Entity;

namespace FluentModelBuilder.Conventions.Entities
{
    public class EntityConvention : IModelBuilderConvention
    {
        public EntityConventionOptions Options { get; } = new EntityConventionOptions();
        public void Apply(ModelBuilder builder)
        {
            foreach(var convention in Options.ModelBuilderConventions)
                convention.Apply(builder);
        }
    }
}
