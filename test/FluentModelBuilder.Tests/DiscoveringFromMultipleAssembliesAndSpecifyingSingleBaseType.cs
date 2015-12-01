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
    public class DiscoveringFromMultipleAssembliesAndSpecifyingSingleBaseType : TestBase
    {
        public DiscoveringFromMultipleAssembliesAndSpecifyingSingleBaseType(ModelFixture fixture) : base(fixture)
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
                                    .BaseType<EntityBase>()))
                .WithInMemoryDatabase();
        }

        [Fact]
        public void AddsCorrectNumberOfEntities()
        {
            Assert.Equal(3, Model.GetEntityTypes().OrderBy(x => x.Name).Count());
        }

        [Theory]
        [InlineData(typeof(EntityOne), 1)]
        [InlineData(typeof(EntityTwo), 2)]
        [InlineData(typeof(EntityOneWannabe), 0)]
        public void AddsCorrectEntities(Type expected, int index)
        {
            Assert.Equal(expected, Model.GetEntityTypes().OrderBy(x => x.Name).ElementAt(index).ClrType);
        }
    }
}