using System;
using System.Linq;
using FluentModelBuilder.Alterations;
using FluentModelBuilder.Builder;
using FluentModelBuilder.Extensions;
using FluentModelBuilder.TestTarget;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FluentModelBuilder
{
    public class BuildingUsingServiceProvider
    {
        [Fact]
        public void UsesServiceProviderToDisccoverEntities()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<DbContext>((sp, x) => x.UseInMemoryDatabase().UseInternalServiceProvider(sp));
            serviceCollection.ConfigureEntityFramework(f => f.Using().AddAlteration<DependingAlteration>());
            serviceCollection.AddSingleton<Dependency>();
            serviceCollection.AddSingleton<DependingAlteration>();
            var provider = serviceCollection.BuildServiceProvider();

            var dbContext = provider.GetService<DbContext>();

            var model = dbContext.Model;

            Assert.Equal(typeof(EntityOne), model.GetEntityTypes().First().ClrType);
        }

        private class Dependency
        {
            public Type GetEntity => typeof(EntityOne);
        }

        private class DependingAlteration : IAutoModelBuilderAlteration
        {
            public void Alter(AutoModelBuilder builder)
            {
                builder.Override<EntityOne>();
            }
        }
        
    }
}