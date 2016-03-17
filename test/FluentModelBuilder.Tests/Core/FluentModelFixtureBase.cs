using FluentModelBuilder.Configuration;
using FluentModelBuilder.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FluentModelBuilder.Tests.Core
{
    public abstract class FluentModelFixtureBase<TContext> : InMemoryModelFixtureBase<TContext>
        where TContext : DbContext
    {
        protected override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            services.ConfigureEntityFramework(ConfigureMappings);
        }

        protected abstract void ConfigureMappings(FluentModelBuilderConfiguration configuration);
    }
}