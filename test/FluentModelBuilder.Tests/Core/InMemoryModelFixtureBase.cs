using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FluentModelBuilder.Tests.Core
{
    public abstract class InMemoryModelFixtureBase<TContext> : ModelFixtureBase<TContext> where TContext : DbContext
    {
        //protected override void ConfigureServices(IServiceCollection services)
        //{
        //    ConfigureServicesCore(services);
        //}

        //protected abstract void ConfigureServicesCore(IServiceCollection services);

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