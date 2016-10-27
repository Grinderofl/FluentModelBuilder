using FluentModelBuilder.Extensions.Accessors;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluentModelBuilder.Extensions
{
    public static class PropertyBuilderExtensions
    {
        public static PropertyBuilder<TProperty> Access<TProperty>(this PropertyBuilder<TProperty> builder,
            PropertyAccessor accessor)
        {
            
            return builder;
        }
    }
}