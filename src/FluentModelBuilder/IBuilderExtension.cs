using Microsoft.Data.Entity.Infrastructure;

namespace FluentModelBuilder
{
    public interface IBuilderExtension
    {
        void Apply(EntityFrameworkServicesBuilder builder);
    }
}