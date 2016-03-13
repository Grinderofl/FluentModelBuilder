using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
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
            services.AddEntityFrameworkInMemoryDatabase().AddDbContext<TContext>(ConfigureContext);
            ConfigureServicesCore(services);
            //ConfigureEntityFrameworkServices(entityFrameworkServices);

        }

        protected abstract void ConfigureServicesCore(IServiceCollection services);
        //protected abstract void ConfigureEntityFrameworkServices(EntityFrameworkServicesBuilder entityFrameworkServices);
        protected abstract void ConfigureContext(IServiceProvider serviceProvider, DbContextOptionsBuilder builder);
    }
}