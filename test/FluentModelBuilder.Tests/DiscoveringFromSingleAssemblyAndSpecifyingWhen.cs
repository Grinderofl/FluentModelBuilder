using System;
using FluentModelBuilder.Core.Contributors.Extensions;
using FluentModelBuilder.Extensions;
using FluentModelBuilder.InMemory.Extensions;
using FluentModelBuilder.Tests.Core;
using FluentModelBuilder.TestTarget;
using Microsoft.Data.Entity;
using Xunit;
using System.Linq;

namespace FluentModelBuilder.Tests
{
    // Extension test.
    // TODO: Technical debt/move extension tests into their own place
    public class DiscoveringFromSingleAssemblyAndSpecifyingWhen : TestBase
    {
        public DiscoveringFromSingleAssemblyAndSpecifyingWhen(ModelFixture fixture) : base(fixture)
        {
        }

        protected override void ConfigureOptions(DbContextOptionsBuilder options)
        {
            options.ConfigureModel()
                .Entities(e => e.Discover(from => from.AssemblyContaining<EntityOne>().When(x => x.BaseType == typeof(EntityBase))))
                .WithInMemoryDatabase();
        }

        [Fact]
        public void AddsCorrectNumberOfEntities()
        {
            Assert.Equal(2, Model.GetEntityTypes().OrderBy(x => x.Name).Count());
        }

        [Theory]
        [InlineData(typeof(EntityOne), 0)]
        [InlineData(typeof(EntityTwo), 1)]
        public void AddsCorrectEntities(Type expected, int index)
        {
            Assert.Equal(expected, Model.GetEntityTypes().OrderBy(x => x.Name).ElementAt(index).ClrType);
        }
    }
}