using FluentModelBuilder.Sqlite.Extensions;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.DependencyInjection.Extensions;

namespace FluentModelBuilder.Sqlite
{
    public class SqliteBuilderExtension : IBuilderExtension
    {
        public void Apply(EntityFrameworkServicesBuilder builder)
        {
            builder.AddSqliteFluentProvider();
        }
    }
}