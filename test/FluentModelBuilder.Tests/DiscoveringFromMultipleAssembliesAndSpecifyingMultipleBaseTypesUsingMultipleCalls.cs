using System;
using FluentModelBuilder.Core.Contributors.Extensions;
using FluentModelBuilder.Extensions;
using FluentModelBuilder.InMemory;
using FluentModelBuilder.InMemory.Extensions;
using FluentModelBuilder.Tests.Core;
using FluentModelBuilder.Tests.Entities;
using FluentModelBuilder.TestTarget;
using Microsoft.Data.Entity;
using Xunit;
using System.Linq;

namespace FluentModelBuilder.Tests
{
    public class DiscoveringFromMultipleAssembliesAndSpecifyingMultipleBaseTypesUsingMultipleCalls : TestBase
    {
        public DiscoveringFromMultipleAssembliesAndSpecifyingMultipleBaseTypesUsingMultipleCalls(ModelFixture fixture) : base(fixture)
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
                                    .BaseType<EntityBase>())
                            .Discover(
                                from => 
                                    from
                                        .AssemblyContaining<EntityOneWannabe>()
                                        .BaseType<EntityBaseWannabe>()))
                .WithInMemoryDatabase();
        }

        [Fact]
        public void AddsCorrectNumberOfEntities()
        {
            Assert.Equal(4, Model.GetEntityTypes().OrderBy(x => x.Name).Count());
        }

        [Theory]
        [InlineData(typeof(EntityOne), 2)]
        [InlineData(typeof(EntityTwo), 3)]
        [InlineData(typeof(EntityBaseWannabe), 0)]
        [InlineData(typeof(EntityTwoWannabe), 1)]
        public void AddsCorrectEntities(Type expected, int index)
        {
            Assert.Equal(expected, Model.GetEntityTypes().OrderBy(x => x.Name).ElementAt(index).ClrType);
        }
    }
}