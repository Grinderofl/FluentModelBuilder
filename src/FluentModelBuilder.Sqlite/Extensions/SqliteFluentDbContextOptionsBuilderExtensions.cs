using System.Data.Common;
using FluentModelBuilder.Extensions;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;

namespace FluentModelBuilder.Sqlite.Extensions
{
    public static class SqliteFluentDbContextOptionsBuilderExtensions
    {
        /// <summary>
        /// Configures DbContext to use Sqlite with FluentModelBuilder
        /// </summary>
        /// <param name="builder"><see cref="FluentDbContextOptionsBuilder"/></param>
        /// <param name="connectionString">Connectionstring to use with the database</param>
        /// <returns><see cref="FluentDbContextOptionsBuilder"/></returns>
        public static FluentDbContextOptionsBuilder WithSqliteDatabase(this FluentDbContextOptionsBuilder builder, string connectionString)
        {
            builder.OptionsBuilder.UseSqlite(connectionString);
            return builder.WithExtension<SqliteBuilderExtension>();
        }

        /// <summary>
        /// Configures DbContext to use Sqlite with FluentModelBuilder
        /// </summary>
        /// <param name="builder"><see cref="FluentDbContextOptionsBuilder"/></param>
        /// <param name="connection">DbConnection to use with the database</param>
        /// <returns><see cref="FluentDbContextOptionsBuilder"/></returns>
        public static FluentDbContextOptionsBuilder WithSqliteDatabase(this FluentDbContextOptionsBuilder builder, DbConnection connection)
        {
            builder.OptionsBuilder.UseSqlite(connection);
            return builder.WithExtension<SqliteBuilderExtension>();
        }
    }
}