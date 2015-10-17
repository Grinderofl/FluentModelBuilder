using System.Data.Common;
using FluentModelBuilder.Extensions;
using Microsoft.Data.Entity;

namespace FluentModelBuilder.SqlServer.Extensions
{
    public static class SqlServerFluentDbContextOptionsBuilderExtensions
    {
        public static FluentDbContextOptionsBuilder WithSqlServerDatabase(this FluentDbContextOptionsBuilder builder, string connectionString)
        {
            builder.OptionsBuilder.UseSqlServer(connectionString);
            return builder.WithExtension<SqlServerBuilderExtension>();
        }

        public static FluentDbContextOptionsBuilder WithSqlServerDatabase(this FluentDbContextOptionsBuilder builder, DbConnection connection)
        {
            builder.OptionsBuilder.UseSqlServer(connection);
            return builder.WithExtension<SqlServerBuilderExtension>();
        }
    }
}