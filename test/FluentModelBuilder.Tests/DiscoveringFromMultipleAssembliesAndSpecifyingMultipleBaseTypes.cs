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
            Assert.Equal(5, Model.GetEntityTypes().Count());
        }

        [Theory]
        [InlineData(typeof(EntityOne), 0)]
        [InlineData(typeof(EntityTwo), 1)]
        [InlineData(typeof(EntityBaseWannabe), 2)]
        [InlineData(typeof(EntityTwoWannabe), 4)]
        [InlineData(typeof(EntityOneWannabe), 3)]
        public void AddsCorrectEntities(Type expected, int index)
        {
            Assert.Equal(expected, Model.GetEntityTypes().ElementAt(index).ClrType);
        }
    }
}