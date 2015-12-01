using FluentModelBuilder.InMemory.Extensions;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;

namespace FluentModelBuilder.InMemory
{
    public class InMemoryBuilderExtension : IBuilderExtension
    {
        public void Apply(EntityFrameworkServicesBuilder builder)
        {
            builder.AddInMemoryFluentModelBuilder();
        }
    }
}