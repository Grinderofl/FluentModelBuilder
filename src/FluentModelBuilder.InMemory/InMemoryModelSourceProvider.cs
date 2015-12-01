using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Infrastructure.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;


namespace FluentModelBuilder.InMemory
{
    public class InMemoryModelSourceProvider : IModelSourceProvider
    {
        public void ApplyServices(EntityFrameworkServicesBuilder services)
        {
            services.AddInMemoryDatabase();
            services.GetInfrastructure().Replace(ServiceDescriptor.Singleton<InMemoryModelSource, InMemoryFluentModelSource>());
        }
    }
}