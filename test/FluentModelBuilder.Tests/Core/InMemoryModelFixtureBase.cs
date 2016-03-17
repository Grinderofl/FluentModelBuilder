using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FluentModelBuilder.Tests.Core
{
    public abstract class InMemoryModelFixtureBase<TContext> : ModelFixtureBase<TContext> where TContext : DbContext
    {
        protected override void ConfigureContext(IServiceProvider provider, DbContextOptionsBuilder builder)
        {
            builder.UseInMemoryDatabase().UseInternalServiceProvider(provider);
        }

    }
}