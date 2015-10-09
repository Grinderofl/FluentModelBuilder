using System.Linq;
using FluentModelBuilder.Conventions.Core.Options.Extensions;
using FluentModelBuilder.Extensions;
using FluentModelBuilder.Options;
using FluentModelBuilder.TestTarget;
using Xunit;

namespace FluentModelBuilder.Tests
{
    public class AddingSingleOverrideToModel : ClassFixture<AddingSingleOverrideToModel.AddingSingleOverrideToModelFixture>
    {

        public class AddingSingleOverrideToModelFixture : ModelFixtureBase
        {
            protected override void ConfigureOptions(FluentModelBuilderOptions options)
            {
                options.Overrides(x => x.Add<EntityOneOverride>());
            }
        }

        [Fact]
        public void ContainsCorrectEntity()
        {
            Assert.Equal(1, Model.EntityTypes.Count);
            Assert.Equal(typeof(EntityOne), Model.EntityTypes[0].ClrType);
        }

        [Fact]
        public void ContainsCorrectProperties()
        {
            var properties = Model.EntityTypes[0].GetProperties().OrderBy(x => x.Name).ToList();
            Assert.Equal("Id", properties[0].Name);
            Assert.Equal("NotIgnored", properties[1].Name);
        }

        [Fact]
        public void DoesNotContainIgnoredProperty()
        {
            Assert.False(Model.EntityTypes[0].GetProperties().Any(x => x.Name == "IgnoredInOverride"));
        }

        public AddingSingleOverrideToModel(AddingSingleOverrideToModelFixture fixture) : base(fixture)
        {
        }
    }


}