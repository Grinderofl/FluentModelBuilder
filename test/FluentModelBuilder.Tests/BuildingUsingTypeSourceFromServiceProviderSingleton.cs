using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using FluentModelBuilder.Alterations;
using FluentModelBuilder.Builder;
using FluentModelBuilder.Extensions;
using FluentModelBuilder.TestTarget;
using FluentModelBuilder.Conventions;
using FluentModelBuilder.Builder.Sources;

namespace FluentModelBuilder
{
    public class BuildingUsingTypeSourceFromServiceProviderSingleton
    {
        [Fact]
        public void UsesServiceProviderToDiscoverEntities()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<DbContext>((sp, x) => x.UseInMemoryDatabase().UseInternalServiceProvider(sp));
            serviceCollection.ConfigureEntityFramework(f => f.Using<DependingSource>());
            serviceCollection.AddSingleton<Dependency>();
            var provider = serviceCollection.BuildServiceProvider();

            var dbContext = provider.GetService<DbContext>();

            var model = dbContext.Model;

            Assert.Equal(typeof(EntityOne), model.GetEntityTypes().First().ClrType);
        }

        private class Dependency
        {
            public Type GetEntity => typeof(EntityOne);
        }

        private class DependingSource : ITypeSource
        {
            private Dependency _dependency;

            public DependingSource(Dependency dependency)
            {
                _dependency = dependency;
            }

            public IEnumerable<Type> GetTypes()
            {
                yield return _dependency.GetEntity;
            }
        }

    }
}