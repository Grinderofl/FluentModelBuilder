using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FluentModelBuilder.SqlServer
{
    public class SqlServerModelSourceProvider : IModelSourceProvider
    {
        public void ApplyServices(EntityFrameworkServicesBuilder services)
        {
            services.AddSqlServer();
            services.GetInfrastructure()
                .Replace(ServiceDescriptor.Singleton<SqlServerModelSource, SqlServerFluentModelSource>());
        }
    }
}