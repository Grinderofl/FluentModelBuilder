using FluentModelBuilder.Configuration;
using Microsoft.Data.Entity.Metadata.Conventions.Internal;
using Microsoft.Data.Entity.Metadata.Internal;

namespace FluentModelBuilder.Conventions
{
    public class FluentModelBuilderConvention : IModelConvention
    {
        private readonly FluentModelBuilderConfiguration _configuration;

        public FluentModelBuilderConvention(FluentModelBuilderConfiguration configuration)
        {
            _configuration = configuration;
        }

        public InternalModelBuilder Apply(InternalModelBuilder modelBuilder)
        {
            _configuration.Apply(modelBuilder);
            return modelBuilder;
        }

    }
}