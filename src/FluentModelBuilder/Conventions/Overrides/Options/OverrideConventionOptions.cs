using System.Collections.Generic;

namespace FluentModelBuilder.Conventions.Overrides.Options
{
    public class OverrideConventionOptions
    {
        public IList<IModelBuilderConvention> ModelBuilderConventions { get; } = new List<IModelBuilderConvention>();
    }
}
