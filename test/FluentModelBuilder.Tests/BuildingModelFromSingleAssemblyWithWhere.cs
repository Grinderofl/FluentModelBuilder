using System;
using System.Linq;
using System.Reflection;
using FluentModelBuilder.Configuration;
using FluentModelBuilder.Tests.Core;
using FluentModelBuilder.TestTarget;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FluentModelBuilder.Tests
{
    public class BuildingModelFromSingleAssemblyWithWhere : TestBase<SingleAssemblyWithWhereFixture, DbContext>
    {
        public BuildingModelFromSingleAssemblyWithWhere(SingleAssemblyWithWhereFixture fixture) : base(fixture)
        {
        }

        [Theory]
        [InlineData(0, typeof(EntityOne))]
        [InlineData(1, typeof(EntityTwo))]
        public void MapsEntity(int index, Type expectedType)
        {
            Assert.Equal(expectedType, EntityTypes.ElementAt(index).ClrType);
        }

        [Theory]
        [InlineData(0, 0, "Id")]
        [InlineData(0, 1, "IgnoredInOverride")]
        [InlineData(0, 2, "NotIgnored")]

        [InlineData(1, 0, "Id")]
        public void MapsEntityProperty(int elementIndex, int propertyIndex, string name)
        {
            var properties = GetProperties(elementIndex);
            Assert.Equal(name, properties.ElementAt(propertyIndex).Name);
        }
    }

    public class SingleAssemblyWithWhereFixture : FluentModelFixtureBase<DbContext>
    {
        protected override void ConfigureMappings(FluentModelBuilderConfiguration configuration)
        {
            configuration.Add(
                From.AssemblyOf<EntityBase>().Where(type => type.GetTypeInfo().IsSubclassOf(typeof (EntityBase))));
        }
    }
}