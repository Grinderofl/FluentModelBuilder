using System.Collections.Generic;
using ConventionModelBuilder.Conventions;
using ConventionModelBuilder.Sources;

namespace ConventionModelBuilder.Options
{
    public class ConventionModelBuilderOptions
    {
        public LinkedList<IModelBuilderConvention> Conventions { get; } = new LinkedList<IModelBuilderConvention>();
        public IConventionSetSource ConventionSetSource { get; set; } = new DefaultConventionSetSource();
        public IModelBuilderSource ModelBuilderSource { get; set; } = new DefaultModelBuilderSource();
        public IModelSource ModelSource { get; set; } = new DefaultModelSource();
        public IConventionApplier ConventionApplier { get; set; } = new DefaultConventionApplier();
    }
}
