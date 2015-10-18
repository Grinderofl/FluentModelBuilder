using FluentModelBuilder.Extensions;
using Microsoft.Data.Entity.Infrastructure;

namespace FluentModelBuilder.SqlServer.Extensions
{
    public static class SqlServerFluentEntityFrameworkServicesBuilderExtensions
    {
        public static EntityFrameworkServicesBuilder AddSqlServerFluentProvider(this EntityFrameworkServicesBuilder builder)
        {
            return builder.AddModelSourceProvider<SqlServerModelSourceProvider>();
        }
    }
}