using FluentModelBuilder.Configuration;
using FluentModelBuilder.Extensions;
using Microsoft.Data.Entity;
using Microsoft.Extensions.DependencyInjection;

namespace FluentModelBuilder.Tests.Core
{
    public abstract class FluentModelFixtureBase<TContext> : InMemoryModelFixtureBase<TContext>
        where TContext : DbContext
    {
        protected override void ConfigureServicesCore(IServiceCollection services)
        {
            services.ConfigureEntityFramework(ConfigureMappings);
        }

        protected abstract void ConfigureMappings(FluentModelBuilderConfiguration configuration);
    }
}