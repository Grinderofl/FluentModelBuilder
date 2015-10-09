using Microsoft.Data.Entity;

namespace FluentModelBuilder.Conventions.Entities
{
    public class RemoveEntityConvention<T> : IModelBuilderConvention
    {
        public void Apply(ModelBuilder builder)
        {
            var entityType = builder.Model.GetEntityType(typeof (T));
            if (entityType != null)
                builder.Model.RemoveEntityType(entityType);
        }
    }
}