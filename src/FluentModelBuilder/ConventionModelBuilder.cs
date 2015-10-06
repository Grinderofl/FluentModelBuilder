using FluentModelBuilder.Options;
using Microsoft.Data.Entity.Metadata;

namespace FluentModelBuilder
{
    /// <summary>
    /// Builds model using conventions
    /// </summary>
    public class FluentModelBuilder
    {
        private readonly FluentModelBuilderOptions _options;
        
        public FluentModelBuilder(FluentModelBuilderOptions options)
        {
            _options = options;
        }

        public IModel Build()
        {
            var model = _options.ModelSource.CreateModel(_options);
            var conventionSet = _options.ConventionSetSource.CreateConventionSet(_options);
            var modelBuilder = _options.ModelBuilderSource.CreateModelBuilder(_options, conventionSet, model);
            _options.ConventionApplier.Apply(modelBuilder, _options);
            return model;
        }
    }
}
