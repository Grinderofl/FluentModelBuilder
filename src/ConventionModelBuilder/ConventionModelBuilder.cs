using ConventionModelBuilder.Options;
using Microsoft.Data.Entity.Metadata;

namespace ConventionModelBuilder
{
    public class ConventionModelBuilder
    {
        private readonly ConventionModelBuilderOptions _options;
        
        public ConventionModelBuilder(ConventionModelBuilderOptions options)
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
