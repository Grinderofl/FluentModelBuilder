using System.Collections.Generic;

namespace FluentModelBuilder.Conventions.Core.Options
{
    public class EntityConventionOptions
    {
        public IList<IModelBuilderConvention> ModelBuilderConventions { get; } = new List<IModelBuilderConvention>();
    }
}
