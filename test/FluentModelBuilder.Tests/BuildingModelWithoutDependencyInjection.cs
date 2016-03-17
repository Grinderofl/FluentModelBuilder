using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentModelBuilder.Configuration;
using FluentModelBuilder.Extensions;
using FluentModelBuilder.Tests.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Xunit;

namespace FluentModelBuilder.Tests
{
    public class BuildingModelWithoutDependencyInjection
    {
        [Fact]
        public void BuildsModel()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<DbContext>(
                c => c.UseInMemoryDatabase().Configure(mappings => mappings.Add(From.Empty().Override<SingleEntity>())));
            serviceCollection.Replace(
                ServiceDescriptor.Scoped<DbContext>(s =>
                {
                    var options = s.GetService<DbContextOptions>();
                    try
                    {
                        var instance = ActivatorUtilities.CreateInstance<DbContext>(s, options);

                        return instance;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }));
            var provider = serviceCollection.BuildServiceProvider();

            var context = provider.GetService<DbContext>();
            var model = context.Model;
            var entity = model.GetEntityTypes().First();

            Assert.Equal(typeof(SingleEntity), entity.ClrType);
        }
    }
}
