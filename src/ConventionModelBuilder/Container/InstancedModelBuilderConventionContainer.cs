using Microsoft.Data.Entity;
using Microsoft.Framework.DependencyInjection;

namespace ConventionModelBuilder.Container
{
    public class InstancedModelBuilderConventionContainer : ModelBuilderConventionContainer
    {
        internal readonly IModelBuilderConvention Instance;

        public InstancedModelBuilderConventionContainer(IModelBuilderConvention instance)
        {
            Instance = instance;
        }

        protected override void ApplyCore(ModelBuilder builder)
        {
            Instance.Apply(builder);
        }
    }
}