using System;
using Microsoft.Extensions.DependencyInjection;

namespace FluentModelBuilder.Tests.Core
{
    public abstract class FixtureBase
    {
        protected IServiceProvider Provider;

        protected FixtureBase()
        {
            var services = new ServiceCollection();

            Configure(services);
            Provider = services.BuildServiceProvider();
        }

        private void Configure(IServiceCollection services)
        {
            ConfigureServices(services);
        }

        protected abstract void ConfigureServices(IServiceCollection services);
    }
}