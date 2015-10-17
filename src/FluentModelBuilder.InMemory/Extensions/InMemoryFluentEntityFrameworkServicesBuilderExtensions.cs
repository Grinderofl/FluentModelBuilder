using FluentModelBuilder.Extensions;
using Microsoft.Data.Entity.Infrastructure;

namespace FluentModelBuilder.InMemory.Extensions
{
    public static class InMemoryFluentEntityFrameworkServicesBuilderExtensions
    {
        public static EntityFrameworkServicesBuilder AddInMemoryFluentProvider(this EntityFrameworkServicesBuilder builder)
        {
            return builder.AddModelSourceProvider<InMemoryModelSourceProvider>();
        }
    }
}