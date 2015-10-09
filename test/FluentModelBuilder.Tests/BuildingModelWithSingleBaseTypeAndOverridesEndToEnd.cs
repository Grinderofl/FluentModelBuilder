using System.Linq;
using FluentModelBuilder.Extensions;
using FluentModelBuilder.Options.Extensions;
using FluentModelBuilder.TestTarget;
using Microsoft.Data.Entity;
using Microsoft.Framework.DependencyInjection;
using Xunit;

namespace FluentModelBuilder.Tests
{
    public class BuildingModelWithSingleBaseTypeAndOverridesEndToEnd : IClassFixture<BuildingModelWithSingleBaseTypeAndOverridesEndToEnd.Fixture>
    {
        private readonly Fixture _fixture;

        public BuildingModelWithSingleBaseTypeAndOverridesEndToEnd(Fixture fixture)
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