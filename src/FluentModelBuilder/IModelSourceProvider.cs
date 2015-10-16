using Microsoft.Data.Entity.Infrastructure;

namespace FluentModelBuilder
{
    public interface IModelSourceProvider
    {
        void ApplyServices(EntityFrameworkServicesBuilder builder);
    }
}