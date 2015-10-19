using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Infrastructure.Internal;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.DependencyInjection.Extensions;

namespace FluentModelBuilder.InMemory
{
    public class InMemoryModelSourceProvider : IModelSourceProvider
    {
        public void ApplyServices(EntityFrameworkServicesBuilder services)
        {
            services.AddInMemoryDatabase();
            services.GetService().Replace(ServiceDescriptor.Singleton<InMemoryModelSource, InMemoryFluentModelSource>());
        }
    }
}