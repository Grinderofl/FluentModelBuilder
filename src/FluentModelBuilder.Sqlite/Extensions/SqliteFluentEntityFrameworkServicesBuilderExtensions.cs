using FluentModelBuilder.Extensions;
using Microsoft.Data.Entity.Infrastructure;

namespace FluentModelBuilder.Sqlite.Extensions
{
    public static class SqliteFluentEntityFrameworkServicesBuilderExtensions
    {
        /// <summary>
        /// Adds required services for Sqlite FluentModelBuilder
        /// </summary>
        /// <param name="builder"><see cref="EntityFrameworkServicesBuilder"/></param>
        /// <returns><see cref="EntityFrameworkServicesBuilder"/></returns>
        public static EntityFrameworkServicesBuilder AddSqliteFluentModelBuilder(this EntityFrameworkServicesBuilder builder)
        {
            return builder.AddFluentModelBuilder<SqliteModelSourceProvider>();
        }
    }
}