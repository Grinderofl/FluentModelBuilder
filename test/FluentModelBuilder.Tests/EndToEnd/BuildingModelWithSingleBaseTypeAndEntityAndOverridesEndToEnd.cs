using System.Linq;
using FluentModelBuilder.Conventions.EntityConvention.Options.Extensions;
using FluentModelBuilder.Extensions;
using FluentModelBuilder.Options.Extensions;
using FluentModelBuilder.TestTarget;
using Microsoft.Data.Entity;
using Microsoft.Framework.DependencyInjection;
using Xunit;

namespace FluentModelBuilder.Tests
{
    public class BuildingModelWithSingleBaseTypeAndEntityAndOverridesEndToEnd : IClassFixture<BuildingModelWithSingleBaseTypeAndEntityAndOverridesEndToEnd.Fixture>
    {
        private readonly Fixture _fixture;

        public BuildingModelWithSingleBaseTypeAndEntityAndOverridesEndToEnd(Fixture fixture)
        {
            _fixture = fixture;
        }

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
                        c.DiscoverEntities(x => x.WithBaseType<EntityBase>().FromAssemblyContaining<EntityOneOverride>());
                        c.AddEntity<EntityWithNoBaseType>();
                        c.DiscoverOverrides(x => x.FromAssemblyContaining<EntityOneOverride>());
                    });
                });
                Context = collection.BuildServiceProvider().GetService<DbContext>();
            }
        }

        [Fact]
        public void DoesNotAddAbstractEntitiesToModel()
        {
            Assert.False(_fixture.Context.Model.EntityTypes.Any(x => x.ClrType == typeof(EntityBase)));
        }

        [Fact]
        public void AddsEntityOneToModel()
        {
            Assert.True(_fixture.Context.Model.EntityTypes.Any(x => x.ClrType == typeof(EntityOne)));
        }

        [Fact]
        public void AddsEntityTwoToModel()
        {
            Assert.True(_fixture.Context.Model.EntityTypes.Any(x => x.ClrType == typeof(EntityTwo)));
        }

        [Fact]
        public void AddsEntityWithNoBaseToModel()
        {
            Assert.True(_fixture.Context.Model.EntityTypes.Any(x => x.ClrType == typeof(EntityWithNoBaseType)));
        }

        [Fact]
        public void AddsIdPropertyToModel()
        {
            Assert.True(_fixture.Context.Model.EntityTypes.Any(c => c.GetProperties().Any(p => p.Name == "Id")));
        }

        [Fact]
        public void AddsNotIgnoredPropertyToModel()
        {
            Assert.True(_fixture.Context.Model.EntityTypes.Any(c => c.GetProperties().Any(p => p.Name == "NotIgnored")));
        }

        [Fact]
        public void DoesNotAddIgnoredPropertiesToModel()
        {
            Assert.False(_fixture.Context.Model.EntityTypes.Any(c => c.GetProperties().Any(p => p.Name == "IgnoredInOverride")));
        }
    }
}