using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Internal;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.DependencyInjection.Extensions;

namespace FluentModelBuilder.SqlServer
{
    public class SqlServerModelSourceProvider : IModelSourceProvider
    {
        public void ApplyServices(EntityFrameworkServicesBuilder services)
        {
            services.AddSqlServer();
            services.GetService().Replace(ServiceDescriptor.Singleton<SqlServerModelSource, SqlServerFluentModelSource>());
        }
    }
}