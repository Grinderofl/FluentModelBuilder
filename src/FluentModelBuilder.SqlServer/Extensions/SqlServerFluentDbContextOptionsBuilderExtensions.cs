using System.Data.Common;
using FluentModelBuilder.Extensions;
using Microsoft.Data.Entity;

namespace FluentModelBuilder.SqlServer.Extensions
{
    public static class SqlServerFluentDbContextOptionsBuilderExtensions
    {
        /// <summary>
        /// Configures DbContext to use Microsoft SQL Server with FluentModelBuilder
        /// </summary>
        /// <param name="builder"><see cref="FluentDbContextOptionsBuilder"/></param>
        /// <param name="connectionString">Connectionstring to use with the database</param>
        /// <returns><see cref="FluentDbContextOptionsBuilder"/></returns>
        public static FluentDbContextOptionsBuilder WithSqlServerDatabase(this FluentDbContextOptionsBuilder builder, string connectionString)
        {
            builder.OptionsBuilder.UseSqlServer(connectionString);
            return builder.WithExtension<SqlServerBuilderExtension>();
        }


        /// <summary>
        /// Configures DbContext to use Microsoft SQL Server with FluentModelBuilder
        /// </summary>
        /// <param name="builder"><see cref="FluentDbContextOptionsBuilder"/></param>
        /// <param name="connection">DbConnection to use with the database</param>
        /// <returns><see cref="FluentDbContextOptionsBuilder"/></returns>
        public static FluentDbContextOptionsBuilder WithSqlServerDatabase(this FluentDbContextOptionsBuilder builder, DbConnection connection)
        {
            builder.OptionsBuilder.UseSqlServer(connection);
            return builder.WithExtension<SqlServerBuilderExtension>();
        }
    }
}