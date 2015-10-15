using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Framework.DependencyInjection;

namespace FluentModelBuilder
{
    public class FluentModelBuilderExtension : IDbContextOptionsExtension
    {
        public FluentModelBuilderExtension()
        {
            Entities = new EntitiesBuilder();
        }

        public FluentModelBuilderExtension(FluentModelBuilderExtension copyFrom)
        {
            Entities = copyFrom.Entities;
            ModelSourceBuilder = copyFrom.ModelSourceBuilder;
        }

        public EntitiesBuilder Entities { get; }
        public IModelSourceBuilder ModelSourceBuilder { get; set; }
        
        public void ApplyServices(EntityFrameworkServicesBuilder builder)
        {
            ModelSourceBuilder.ApplyServices(builder);
            builder.AddFluentBuilder();
        }
    }
}