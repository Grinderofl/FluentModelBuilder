using FluentModelBuilder.InMemory.Extensions;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.DependencyInjection.Extensions;

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