using System.Data.Common;
using FluentModelBuilder.Extensions;
using Microsoft.Data.Entity;

namespace FluentModelBuilder.Sqlite.Extensions
{
    public static class SqliteFluentDbContextOptionsBuilderExtensions
    {
        public static FluentDbContextOptionsBuilder WithSqliteDatabase(this FluentDbContextOptionsBuilder builder, string connectionString)
        {
            builder.OptionsBuilder.UseSqlite(connectionString);
            return builder.WithExtension<SqliteBuilderExtension>();
        }

        public static FluentDbContextOptionsBuilder WithSqliteDatabase(this FluentDbContextOptionsBuilder builder, DbConnection connection)
        {
            builder.OptionsBuilder.UseSqlite(connection);
            return builder.WithExtension<SqliteBuilderExtension>();
        }
    }
}