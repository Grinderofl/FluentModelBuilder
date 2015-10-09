using System.Collections.Generic;

namespace FluentModelBuilder.Conventions.Entities.Options
{
    public class EntityConventionOptions
    {
        public IList<IModelBuilderConvention> ModelBuilderConventions { get; } = new List<IModelBuilderConvention>();
    }
}
