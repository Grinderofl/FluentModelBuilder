using System;
using System.Linq;
using FluentModelBuilder.Configuration;
using FluentModelBuilder.Tests.Core;
using FluentModelBuilder.Tests.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using Xunit;

namespace FluentModelBuilder.Tests
{
    public class IdentityContextFixture : FluentModelFixtureBase<IdentityDbContext>
    {
        protected override void ConfigureMappings(FluentModelBuilderConfiguration configuration)
        {
            configuration.Add(From.Empty().Override<SingleEntity>().Override<OtherSingleEntity>());
        }
    }
    
    public class BuildingModelFromIdentityContext : TestBase<IdentityContextFixture, IdentityDbContext>
    {
        public BuildingModelFromIdentityContext(IdentityContextFixture fixture) : base(fixture)
        {
        }

        [Theory]
        [InlineData(0, typeof(OtherSingleEntity))]
        [InlineData(1, typeof(SingleEntity))]
        public void MapsFluentEntity(int index, Type expectedType)
        {
            Assert.Equal(expectedType, EntityTypes.ElementAt(index).ClrType);
        }

        [Theory]
        [InlineData(2, typeof(IdentityRole))]
        [InlineData(3, typeof(IdentityRoleClaim<string>))]
        [InlineData(4, typeof(IdentityUser))]
        [InlineData(5, typeof(IdentityUserClaim<string>))]
        [InlineData(6, typeof(IdentityUserLogin<string>))]
        [InlineData(7, typeof(IdentityUserRole<string>))]
        public void MapsIdentityEntity(int index, Type expectedType)
        {
            Assert.Equal(expectedType, EntityTypes.ElementAt(index).ClrType);
        }
    }
}