using FluentModelBuilder.Extensions;
using Microsoft.Data.Entity.Infrastructure;

namespace FluentModelBuilder.SqlServer.Extensions
{
    public static class SqlServerFluentEntityFrameworkServicesBuilderExtensions
    {
        /// <summary>
        /// Adds required services for Microsoft SQL Server FluentModelBuilder
        /// </summary>
        /// <param name="builder"><see cref="EntityFrameworkServicesBuilder"/></param>
        /// <returns><see cref="EntityFrameworkServicesBuilder"/></returns>
        public static EntityFrameworkServicesBuilder AddSqlServerFluentProvider(this EntityFrameworkServicesBuilder builder)
        {
            return builder.AddModelSourceProvider<SqlServerModelSourceProvider>();
        }
    }
}