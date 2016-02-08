using System.Linq;
using FluentModelBuilder.Alterations;
using FluentModelBuilder.Builder;
using FluentModelBuilder.Configuration;
using FluentModelBuilder.Tests.Core;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xunit;

namespace FluentModelBuilder.Tests
{
    public class BuildingModelFromIdentityContextAndOverridingIdentity : TestBase<IdentityContextOverridingIdentityFixture, IdentityDbContext>
    {
        public BuildingModelFromIdentityContextAndOverridingIdentity(IdentityContextOverridingIdentityFixture fixture) : base(fixture)
        {
        }

        [Theory]
        [InlineData("AccessFailedCount", 2, 0)]
        [InlineData("ConcurrencyStamp", 2, 1)]
        [InlineData("Email", 2, 2)]
        [InlineData("EmailConfirmed", 2, 3)]
        [InlineData("Id", 2, 4)]
        [InlineData("LockoutEnabled", 2, 5)]
        [InlineData("LockoutEnd", 2, 6)]
        [InlineData("MyProperty", 2, 7)]
        [InlineData("NormalizedEmail", 2, 8)]
        [InlineData("NormalizedUserName", 2, 9)]
        [InlineData("PasswordHash", 2, 10)]
        [InlineData("PhoneNumber", 2, 11)]
        [InlineData("PhoneNumberConfirmed", 2, 12)]
        [InlineData("SecurityStamp", 2, 13)]
        [InlineData("TwoFactorEnabled", 2, 14)]
        [InlineData("UserName", 2, 15)]
        public void MapsEntityProperty(string propertyName, int elementIndex, int propertyIndex)
        {
            Assert.Equal(propertyName, GetElementProperty(elementIndex, propertyIndex).Name);
        }

        [Fact]
        public void UpdatesEntityProperty()
        {
            Assert.Equal(666, GetElementProperty(2, 2).FindAnnotation("MaxLength").Value);
        }
    }

    public class IdentityContextOverridingIdentityFixture : FluentModelFixtureBase<IdentityDbContext>
    {
        internal class IdentityUserOverride : IEntityTypeOverride<IdentityUser>
        {
            public void Override(EntityTypeBuilder<IdentityUser> mapping)
            {
                mapping.Property<string>("MyProperty");
                mapping.Property(x => x.Email).HasMaxLength(666); // This should stand out
            }
        }

        protected override void ConfigureMappings(FluentModelBuilderConfiguration configuration)
        {
            configuration.Add(From.Empty().Override(typeof (IdentityUserOverride)).Scope(BuilderScope.PostModelCreating));
        }
    }
}