using FluentModelBuilder.Sqlite.Extensions;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;


namespace FluentModelBuilder.Sqlite
{
    public class SqliteBuilderExtension : IBuilderExtension
    {
        public void Apply(EntityFrameworkServicesBuilder builder)
        {
            builder.AddSqliteFluentModelBuilder();
        }
    }
}