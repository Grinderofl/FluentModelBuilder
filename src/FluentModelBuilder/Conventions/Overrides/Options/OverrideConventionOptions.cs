using System.Collections.Generic;

namespace FluentModelBuilder.Conventions.Core.Options
{
    public class OverrideConventionOptions
    {
        public IList<IModelBuilderConvention> ModelBuilderConventions { get; } = new List<IModelBuilderConvention>();
    }
}
