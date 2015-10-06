using System.Linq;
using ConventionModelBuilder.Conventions.Options;
using ConventionModelBuilder.Conventions.Options.Extensions;
using ConventionModelBuilder.Extensions;
using ConventionModelBuilder.Options.Extensions;
using ConventionModelBuilder.TestTarget;
using Microsoft.Data.Entity;
using Microsoft.Framework.DependencyInjection;
using Xunit;

namespace ConventionModelBuilder.Tests
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
                    o.BuildModelUsingConventions(c =>
                    {
                        c.AddEntity<EntityWithNoBaseType>();
                        c.AddEntities(e => e.WithBaseType<EntityBase>().FromAssemblyContaining<NotAnEntity>());
                        c.AddOverrides(ov => ov.FromAssemblyContaining<NotAnEntity>());
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
        public void AddsEntitiesToModel()
        {
            Assert.True(_fixture.Context.Model.EntityTypes.Any(x => x.ClrType == typeof(EntityOne)));
            Assert.True(_fixture.Context.Model.EntityTypes.Any(x => x.ClrType == typeof(EntityTwo)));
            Assert.True(_fixture.Context.Model.EntityTypes.Any(x => x.ClrType == typeof(EntityWithNoBaseType)));
        }

        [Fact]
        public void AddsPropertiesToModel()
        {
            Assert.True(_fixture.Context.Model.EntityTypes.Any(c => c.GetProperties().Any(p => p.Name == "NotIgnored")));
            Assert.True(_fixture.Context.Model.EntityTypes.Any(c => c.GetProperties().Any(p => p.Name == "Id")));
        }

        [Fact]
        public void DoesNotAddIgnoredPropertiesToModel()
        {
            Assert.False(_fixture.Context.Model.EntityTypes.Any(c => c.GetProperties().Any(p => p.Name == "IgnoredInOverride")));
        }
    }
}