using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluentModelBuilder.Conventions
{
    public class IgnoreReadOnlyPropertiesConvention : AbstractEntityConvention
    {
        protected override void Apply(EntityTypeBuilder entityType)
        {
            foreach (var property in entityType.Metadata.GetProperties().Where(x => !x.PropertyInfo.CanWrite))
            {
                entityType.Ignore(property.Name);
            }
        }
    }
}