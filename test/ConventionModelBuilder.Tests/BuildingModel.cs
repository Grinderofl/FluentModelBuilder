using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConventionModelBuilder.Conventions.Options.Extensions;
using ConventionModelBuilder.Extensions;
using ConventionModelBuilder.Options.Extensions;
using ConventionModelBuilder.TestTarget;
using Microsoft.Data.Entity;
using Microsoft.Framework.DependencyInjection;
using Xunit;

namespace ConventionModelBuilder.Tests
{
    public class BuildingModel : IClassFixture<BuildingModel.Fixture>
    {
        public class Fixture
        {
            public IServiceProvider Provider;

            public Fixture()
            {
                var collection = new ServiceCollection();
                collection.AddEntityFramework().AddDbContext<DbContext>(o =>
                {
                    o.BuildModelUsingConventions(c =>
                    {
                        c.AddEntities(e => e.WithBaseType<EntityBase>().FromAssemblyContaining<NotAnEntity>());
                    });
                });
                Provider = collection.BuildServiceProvider();
            }
        }

        private readonly Fixture _fixture;

        public BuildingModel(Fixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void AddsEntitiesToModel()
        {
            var context = _fixture.Provider.GetService<DbContext>();
            Assert.True(context.Model.EntityTypes.Any(x => x.ClrType == typeof (EntityOne)));
            Assert.True(context.Model.EntityTypes.Any(x => x.ClrType == typeof (EntityTwo)));
        }
    }
}
