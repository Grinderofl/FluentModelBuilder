using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace FluentModelBuilder.Tests.Core
{
    public abstract class ModelFixtureBase<TContext> : FixtureBase where TContext : DbContext
    {
        public IModel Model;

        protected ModelFixtureBase()
        {
            Model = Provider.GetService<TContext>().Model;
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            var entityFrameworkServices = services.AddEntityFramework().AddDbContext<TContext>(ConfigureContext);
            ConfigureEntityFrameworkServices(entityFrameworkServices);
            ConfigureServicesCore(services);
        }

        protected abstract void ConfigureServicesCore(IServiceCollection services);
        protected abstract void ConfigureEntityFrameworkServices(EntityFrameworkServicesBuilder entityFrameworkServices);
        protected abstract void ConfigureContext(DbContextOptionsBuilder builder);
    }
}