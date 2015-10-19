using FluentModelBuilder.Extensions;
using Microsoft.Data.Entity.Infrastructure;

namespace FluentModelBuilder.InMemory.Extensions
{
    public static class InMemoryFluentEntityFrameworkServicesBuilderExtensions
    {
        /// <summary>
        /// Adds required services for In Memory FluentModelBuilder
        /// </summary>
        /// <param name="builder"><see cref="EntityFrameworkServicesBuilder"/></param>
        /// <returns><see cref="EntityFrameworkServicesBuilder"/></returns>
        public static EntityFrameworkServicesBuilder AddInMemoryFluentModelBuilder(this EntityFrameworkServicesBuilder builder)
        {
            return builder.AddModelSourceProvider<InMemoryModelSourceProvider>();
        }
    }
}