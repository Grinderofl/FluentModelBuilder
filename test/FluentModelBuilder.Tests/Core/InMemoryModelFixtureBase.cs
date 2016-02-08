using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FluentModelBuilder.Tests.Core
{
    public abstract class InMemoryModelFixtureBase<TContext> : ModelFixtureBase<TContext> where TContext : DbContext
    {
        protected override void ConfigureContext(DbContextOptionsBuilder builder)
        {
            builder.UseInMemoryDatabase();
        }

        protected override void ConfigureEntityFrameworkServices(EntityFrameworkServicesBuilder entityFrameworkServices)
        {
            entityFrameworkServices.AddInMemoryDatabase();
        }
    }
}