using System;
using System.Linq;
using FluentModelBuilder.Core.Contributors.Extensions;
using FluentModelBuilder.Extensions;
using FluentModelBuilder.InMemory;
using FluentModelBuilder.InMemory.Extensions;
using FluentModelBuilder.Tests.Core;
using FluentModelBuilder.Tests.Entities;
using FluentModelBuilder.TestTarget;
using Microsoft.Data.Entity;
using Xunit;

namespace FluentModelBuilder.Tests
{
    public class DiscoveringFromMultipleAssembliesAndSpecifyingMultipleBaseTypes : TestBase
    {
        public DiscoveringFromMultipleAssembliesAndSpecifyingMultipleBaseTypes(ModelFixture fixture) : base(fixture)
        {
        }

        protected override void ConfigureOptions(DbContextOptionsBuilder options)
        {
            options.ConfigureModel()
                .Entities(
                    e =>
                        e.Discover(
                            from =>
                                from.AssemblyContaining<EntityOne>()
                                    .AssemblyContaining<EntityOneWannabe>()
                                    .BaseType<EntityBase>()
                                    .BaseType<EntityBaseWannabe>()))
                .WithInMemoryDatabase();
        }

        [Fact]
        public void AddsCorrectNumberOfEntities()
        {
            Assert.Equal(5, Model.GetEntityTypes().OrderBy(x => x.Name).Count());
        }

        [Theory]
        [InlineData(typeof(EntityOne), 3)]
        [InlineData(typeof(EntityTwo), 4)]
        [InlineData(typeof(EntityBaseWannabe), 0)]
        [InlineData(typeof(EntityTwoWannabe), 2)]
        [InlineData(typeof(EntityOneWannabe), 1)]
        public void AddsCorrectEntities(Type expected, int index)
        {
            var entityTypes = Model.GetEntityTypes().OrderBy(x => x.Name).ToList();
            Assert.Equal(expected, Model.GetEntityTypes().OrderBy(x => x.Name).ElementAt(index).ClrType);
        }
    }
}