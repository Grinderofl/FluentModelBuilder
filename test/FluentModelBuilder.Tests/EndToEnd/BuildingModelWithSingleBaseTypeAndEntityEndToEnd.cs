using System.Linq;
using FluentModelBuilder.Conventions.Entities.Options.Extensions;
using FluentModelBuilder.Extensions;
using FluentModelBuilder.TestTarget;
using Microsoft.Data.Entity;
using Microsoft.Framework.DependencyInjection;
using Xunit;

namespace FluentModelBuilder.Tests.EndToEnd
{
    public class BuildingModelWithSingleBaseTypeAndEntityEndToEnd : IClassFixture<BuildingModelWithSingleBaseTypeAndEntityEndToEnd.Fixture>
    {
        public class Fixture
        {
            public DbContext Context;

            public Fixture()
            {
                var collection = new ServiceCollection();
                collection.AddEntityFramework().AddDbContext<DbContext>(o =>
                {
                    o.BuildModel(c =>
                    {
                        c.AddEntity<EntityWithNoBaseType>();
                        c.Entities(
                            x => x.Discover(d => d.WithBaseType<EntityBase>().FromAssemblyContaining<NotAnEntity>()));
                    });
                });
                Context = collection.BuildServiceProvider().GetService<DbContext>();
            }
        }

        private readonly Fixture _fixture;

        public BuildingModelWithSingleBaseTypeAndEntityEndToEnd(Fixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void DoesNotAddAbstractEntitiesToModel()
        {
            Assert.False(_fixture.Context.Model.EntityTypes.Any(x => x.ClrType == typeof(EntityBase)));
        }

        [Fact]
        public void AddsEntitiesToModel()
        {
            Assert.True(_fixture.Context.Model.EntityTypes.Any(x => x.ClrType == typeof (EntityOne)));
            Assert.True(_fixture.Context.Model.EntityTypes.Any(x => x.ClrType == typeof (EntityTwo)));
            Assert.True(_fixture.Context.Model.EntityTypes.Any(x => x.ClrType == typeof (EntityWithNoBaseType)));
        }

        [Fact]
        public void AddsPropertiesToModel()
        {
            Assert.True(_fixture.Context.Model.EntityTypes.Any(c => c.GetProperties().Any(p => p.Name == "IgnoredInOverride")));
            Assert.True(_fixture.Context.Model.EntityTypes.Any(c => c.GetProperties().Any(p => p.Name == "NotIgnored")));
            Assert.True(_fixture.Context.Model.EntityTypes.Any(c => c.GetProperties().Any(p => p.Name == "Id")));
        }
    }
}
