using FluentModelBuilder.Conventions.Core.Options;
using Microsoft.Data.Entity;

namespace FluentModelBuilder.Conventions.Core
{
    public class OverrideConvention : IModelBuilderConvention
    {
        public OverrideConventionOptions Options { get; } = new OverrideConventionOptions();
        public void Apply(ModelBuilder builder)
        {
            foreach (var convention in Options.ModelBuilderConventions)
                convention.Apply(builder);
        }
    }
}