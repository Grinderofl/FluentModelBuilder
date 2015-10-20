using FluentModelBuilder.SqlServer.Extensions;
using Microsoft.Data.Entity.Infrastructure;

namespace FluentModelBuilder.SqlServer
{
    public class SqlServerBuilderExtension : IBuilderExtension
    {
        public void Apply(EntityFrameworkServicesBuilder builder)
        {
            builder.AddSqlServerFluentModelBuilder();
        }
    }
}