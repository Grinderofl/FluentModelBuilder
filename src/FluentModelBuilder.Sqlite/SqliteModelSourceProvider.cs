using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Infrastructure.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FluentModelBuilder.Sqlite
{
    public class SqliteModelSourceProvider : IModelSourceProvider
    {
        public void ApplyServices(EntityFrameworkServicesBuilder services)
        {
            services.AddSqlite();
            services.GetInfrastructure().Replace(ServiceDescriptor.Singleton<SqliteModelSource, SqliteFluentModelSource>());
        }
    }
}