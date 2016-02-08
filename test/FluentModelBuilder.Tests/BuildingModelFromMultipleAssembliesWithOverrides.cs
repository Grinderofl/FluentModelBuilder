using System;
using FluentModelBuilder.Configuration;
using FluentModelBuilder.Tests.Core;
using FluentModelBuilder.Tests.Entities;
using FluentModelBuilder.TestTarget;
using Xunit;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FluentModelBuilder.Tests
{
    public class MultipleAssemblyFixtureWithOverrides : FluentModelFixtureBase<DbContext>
    {
        protected override void ConfigureMappings(FluentModelBuilderConfiguration configuration)
        {
            configuration.Add(
                From.AssemblyOf<EntityBase>(new TestConfiguration())
                    .AddEntityAssemblyOf<EntityOneWannabe>()
                    .UseOverridesFromAssemblyOf<EntityBase>()
                    .UseOverridesFromAssemblyOf<EntityOneWannabe>());
        }
    }

    public class BuildingModelFromMultipleAssembliesWithOverrides : TestBase<MultipleAssemblyFixtureWithOverrides, DbContext>
    {
        public BuildingModelFromMultipleAssembliesWithOverrides(MultipleAssemblyFixtureWithOverrides fixture) : base(fixture)
        {
        }

        [Theory]
        [InlineData(0, typeof(EntityOneWannabe))]
        [InlineData(1, typeof(SingleEntity))]
        [InlineData(2, typeof(EntityOne))]
        [InlineData(3, typeof(EntityTwo))]
        public void MapsEntity(int index, Type expectedType)
        {
            Assert.Equal(expectedType, EntityTypes.ElementAt(index).ClrType);
        }

        [Theory]
        [InlineData(0, 0, "Id")]
        [InlineData(0, 1, "LookAtMe")]

        [InlineData(1, 0, "DateProperty")]
        [InlineData(1, 1, "Id")]
        
        [InlineData(2, 0, "Id")]
        [InlineData(2, 1, "NotIgnored")]

        [InlineData(3, 0, "Id")]
        [InlineData(3, 1, "NotProperty")]
        public void MapsEntityProperty(int elementIndex, int propertyIndex, string name)
        {
            var properties = GetProperties(elementIndex);
            Assert.Equal(name, properties.ElementAt(propertyIndex).Name);
        }
    }
}