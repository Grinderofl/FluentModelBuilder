using System;
using FluentModelBuilder.Core.Contributors.Extensions;
using FluentModelBuilder.Extensions;
using FluentModelBuilder.InMemory;
using FluentModelBuilder.InMemory.Extensions;
using FluentModelBuilder.Tests.Core;
using FluentModelBuilder.TestTarget;
using Microsoft.Data.Entity;
using Xunit;

namespace FluentModelBuilder.Tests
{
    public class DiscoveringFromSingleAssemblyAndSpecifyingSingleBaseType : TestBase
    {
        public DiscoveringFromSingleAssemblyAndSpecifyingSingleBaseType(ModelFixture fixture) : base(fixture)
        {
        }

        protected override void ConfigureOptions(DbContextOptionsBuilder options)
        {
            options.ConfigureModel()
                .Entities(e => e.Discover(from => from.AssemblyContaining<EntityOne>().BaseType<EntityBase>()))
                .WithInMemoryDatabase();
        }

        [Fact]
        public void AddsCorrectNumberOfEntities()
        {
            Assert.Equal(2, Model.EntityTypes.Count);
        }

        [Theory]
        [InlineData(typeof(EntityOne), 0)]
        [InlineData(typeof(EntityTwo), 1)]
        public void AddsCorrectEntities(Type expected, int index)
        {
            Assert.Equal(expected, Model.EntityTypes[index].ClrType);
        }
    }
}