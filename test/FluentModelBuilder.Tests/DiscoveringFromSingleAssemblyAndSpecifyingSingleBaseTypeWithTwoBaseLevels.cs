using System;
using System.Linq;
using FluentModelBuilder.Core.Contributors.Extensions;
using FluentModelBuilder.Extensions;
using FluentModelBuilder.InMemory.Extensions;
using FluentModelBuilder.Tests.Core;
using FluentModelBuilder.TestTarget;
using Microsoft.Data.Entity;
using Xunit;

namespace FluentModelBuilder.Tests
{
    public class DiscoveringFromSingleAssemblyAndSpecifyingSingleBaseTypeWithTwoBaseLevels : TestBase
    {
        public DiscoveringFromSingleAssemblyAndSpecifyingSingleBaseTypeWithTwoBaseLevels(ModelFixture fixture) : base(fixture)
        {
        }

        protected override void ConfigureOptions(DbContextOptionsBuilder options)
        {
            options.ConfigureModel()
                .Entities(e => e.Discover(from => from.AssemblyContaining<EntityBaseWithoutId>().BaseType<EntityBaseWithoutId>()))
                .WithInMemoryDatabase();
        }

        [Fact]
        public void AddsCorrectNumberOfEntities()
        {
            Assert.Equal(1, Model.GetEntityTypes().OrderBy(x => x.Name).Count());
        }

        [Theory]
        [InlineData(typeof(EntityWithIntId), 0, typeof(int))]
        public void AddsCorrectEntities(Type expected, int index, Type pkType)
        {
            Assert.Equal(expected, Model.GetEntityTypes().OrderBy(x => x.Name).ElementAt(index).ClrType);
            Assert.Equal(pkType, Model.GetEntityTypes().OrderBy(x => x.Name).ElementAt(index).GetProperties().ElementAt(0).ClrType);
        }
    }
}