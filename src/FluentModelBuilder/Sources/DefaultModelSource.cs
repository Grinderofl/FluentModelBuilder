using FluentModelBuilder.Options;
using Microsoft.Data.Entity.Metadata;

namespace FluentModelBuilder.Sources
{
    public class DefaultModelSource : IModelSource
    {
        public virtual Model CreateModel(FluentModelBuilderOptions options)
        {
            return new Model();
        }
    }
}