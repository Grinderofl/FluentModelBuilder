using System.Linq;
using FluentModelBuilder.Conventions.Assemblies.Options.Extensions;
using FluentModelBuilder.Extensions;
using FluentModelBuilder.Options;
using FluentModelBuilder.TestTarget;
using Microsoft.Data.Entity.Metadata.Builders;
using Xunit;

namespace FluentModelBuilder.Tests.OverrideTests
{
    public class AddingMultipleOverridesToModel : ClassFixture<AddingMultipleOverridesToModel.Fixture>
    {
        public class Fixture : ModelFixtureBase
        {
            protected override void ConfigureOptions(FluentModelBuilderOptions options)
            {
                options.Overrides(x => x.Add<EntityOneOverride>());
                options.Overrides(x => x.Add<EntityOneSecondOverride>());
            }
        }

        public class EntityOneSecondOverride : IEntityTypeOverride<EntityOne>
        {
            public void Configure(EntityTypeBuilder<EntityOne> mapping)
            {
                mapping.Ignore(x => x.NotIgnored);
            }
        }

        [Fact]
        public void ContainsCorrectEntity()
        {
            Assert.Equal(1, Model.EntityTypes.Count);
            Assert.Equal(typeof (EntityOne), Model.EntityTypes[0].ClrType);
        }

        [Fact]
        public void ContainsCorrectProperties()
        {
            var properties = Model.EntityTypes[0].GetProperties().ToList();
            Assert.Equal("Id", properties[0].Name);
        }

        [Fact]
        public void DoesNotContainIgnoredProperty()
        {
            Assert.False(Model.EntityTypes[0].GetProperties().Any(x => x.Name == "IgnoredInOverride"));
            Assert.False(Model.EntityTypes[0].GetProperties().Any(x => x.Name == "NotIgnored"));
        }

        public AddingMultipleOverridesToModel(Fixture fixture) : base(fixture)
        {
        }
    }
}
