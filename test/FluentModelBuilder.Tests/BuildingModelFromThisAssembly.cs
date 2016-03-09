using System;
using System.Linq;
using FluentModelBuilder.Configuration;
using FluentModelBuilder.Tests.Core;
using FluentModelBuilder.Tests.Entities;
using FluentModelBuilder.TestTarget;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FluentModelBuilder.Tests
{
#if NET451
    public class BuildingModelFromThisAssembly : TestBase<ThisAssemblyFixture, DbContext>
    {
        public BuildingModelFromThisAssembly(ThisAssemblyFixture fixture) : base(fixture)
        {
        }

        [Theory]
        [InlineData(0, typeof(EntityOneWannabe))]
        public void MapsEntity(int index, Type expectedType)
        {
            Assert.Equal(expectedType, EntityTypes.ElementAt(index).ClrType);
        }

        [Theory]
        [InlineData(0, 0, "Id")]
        [InlineData(0, 1, "LookAtMe")]
        public void MapsEntityProperty(int elementIndex, int propertyIndex, string name)
        {
            var properties = GetProperties(elementIndex);
            Assert.Equal(name, properties.ElementAt(propertyIndex).Name);
        }
    }

    public class ThisAssemblyFixture : FluentModelFixtureBase<DbContext>
    {
        protected override void ConfigureMappings(FluentModelBuilderConfiguration configuration)
        {
            configuration.Add(From.ThisAssembly(new TestConfiguration()));
        }
    }
#endif
}