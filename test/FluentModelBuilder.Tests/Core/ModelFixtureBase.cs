using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FluentModelBuilder.Tests.Core
{
    public abstract class ModelFixtureBase<TContext> : FixtureBase where TContext : DbContext
    {
        public IModel Model;

        protected ModelFixtureBase()
        {
            var context = Provider.GetService<TContext>();
            Model = context.Model;
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkInMemoryDatabase().AddDbContext<TContext>(ConfigureContext);
            //services.Replace(
            //    ServiceDescriptor.Scoped<TContext>(
            //        provider =>
            //        {
            //            var options = provider.GetService<DbContextOptions>();
            //            try
            //            {
            //                var instance = ActivatorUtilities.CreateInstance<TContext>(provider, options);

            //                return instance;
            //            }
            //            catch (Exception)
            //            {
            //                return null;
            //            }
            //        }));
            //ConfigureServicesCore(services);
            //ConfigureEntityFrameworkServices(entityFrameworkServices);

        }

        //protected abstract void ConfigureServicesCore(IServiceCollection services);
        //protected abstract void ConfigureEntityFrameworkServices(EntityFrameworkServicesBuilder entityFrameworkServices);
        protected abstract void ConfigureContext(IServiceProvider serviceProvider, DbContextOptionsBuilder builder);
    }
}