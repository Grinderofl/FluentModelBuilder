using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using FluentModelBuilder.Alterations;
using FluentModelBuilder.Builder;
using FluentModelBuilder.Extensions;
using FluentModelBuilder.TestTarget;
using FluentModelBuilder.Conventions;

namespace FluentModelBuilder
{
    public class BuildingUsingServiceProviderScoped
    {
        [Fact]
        public void UsesServiceProviderToDisccoverEntities()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<DbContext>((sp, x) => x.UseInMemoryDatabase().UseInternalServiceProvider(sp));
            serviceCollection.ConfigureEntityFramework(f => f.Using().UseConvention<DependingConvention>());
            serviceCollection.AddScoped<Dependency>();
            var provider = serviceCollection.BuildServiceProvider();

            var dbContext = provider.GetService<DbContext>();

            var model = dbContext.Model;

            Assert.Equal(typeof(EntityOne), model.GetEntityTypes().First().ClrType);
        }

        private class Dependency
        {
            public Type GetEntity => typeof(EntityOne);
        }

        private class DependingConvention : IModelBuilderConvention
        {
            private Dependency _dependency;

            public DependingConvention(Dependency dependency)
            {
                _dependency = dependency;
            }

            public void Apply(ModelBuilder builder)
            {
                builder.Entity(_dependency.GetEntity);
            }
        }
        
    }
}