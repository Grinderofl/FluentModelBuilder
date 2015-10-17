using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Infrastructure.Internal;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.DependencyInjection.Extensions;

namespace FluentModelBuilder.Sqlite
{
    public class SqliteModelSourceProvider : IModelSourceProvider
    {
        public void ApplyServices(EntityFrameworkServicesBuilder services)
        {
            services.AddSqlite();
            services.GetService().Replace(ServiceDescriptor.Singleton<SqliteModelSource, SqliteFluentModelSource>());
        }
    }
}