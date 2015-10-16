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
            Extension = copyFrom.Extension;
        }

        public EntitiesBuilder Entities { get; }
        public IBuilderExtension Extension { get; set; }
        
        public void ApplyServices(EntityFrameworkServicesBuilder builder)
        { 
            Extension.Apply(builder);
        }
    }
}