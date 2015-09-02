using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Framework.DependencyInjection;

namespace ConventionModelBuilder.Container
{
    public abstract class ModelBuilderConventionContainer
    {
        public void Apply(ModelBuilder builder)
        {
            ApplyCore(builder);
        }

        protected abstract void ApplyCore(ModelBuilder getServices);
    }
}