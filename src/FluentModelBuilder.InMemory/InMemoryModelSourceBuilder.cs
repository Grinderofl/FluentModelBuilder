using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.InMemory;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.DependencyInjection.Extensions;

namespace FluentModelBuilder.InMemory
{
    public class InMemoryBuilderExtension : IBuilderExtension
    {
        public void ApplyServices(DbContextOptionsBuilder builder)
        {
            builder.UseInMemoryDatabase();
            //builder.AddInMemoryDatabase()
            //    .GetService()
            //    .Replace(ServiceDescriptor.Singleton<InMemoryModelSource, InMemoryFluentModelSource>());
        }
    }
}