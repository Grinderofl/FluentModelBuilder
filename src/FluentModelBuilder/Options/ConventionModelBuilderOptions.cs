using System.Collections.Generic;
using FluentModelBuilder.Conventions;
using FluentModelBuilder.Sources;

namespace FluentModelBuilder.Options
{
    public class FluentModelBuilderOptions
    {
        /// <summary>
        /// Conventions for configuring ModelBuilder
        /// </summary>
        public LinkedList<IModelBuilderConvention> Conventions { get; } = new LinkedList<IModelBuilderConvention>();

        /// <summary>
        /// Source for configuring the initial Model
        /// </summary>
        public IModelSource ModelSource { get; set; } = new DefaultModelSource();

        /// <summary>
        /// Source for creating conventions for the initial Model
        /// </summary>
        public IConventionSetSource ConventionSetSource { get; set; } = new DefaultConventionSetSource();

        /// <summary>
        /// Source for creating the intial ModelBuilder
        /// </summary>
        public IModelBuilderSource ModelBuilderSource { get; set; } = new DefaultModelBuilderSource();

        /// <summary>
        /// Applies conventions to Model
        /// </summary>
        public IConventionApplier ConventionApplier { get; set; } = new DefaultConventionApplier();
    }
}
