using System.Linq;
using FluentModelBuilder.Extensions;
using FluentModelBuilder.Options;
using FluentModelBuilder.TestTarget;
using Microsoft.Data.Entity.Metadata;
using Xunit;

namespace FluentModelBuilder.Tests
{
    public class AddingEntityToAndConfiguringModel : IClassFixture<AddingEntityToAndConfiguringModel.Fixture>
    {
        public class Fixture
        {
            public Fixture()
            {
                var options = new FluentModelBuilderOptions();
                options.AddEntity<EntityOne>(x => x.Ignore(c => c.IgnoredInOverride));
                Model = new FluentModelBuilder(options).Build();
            }

            public IModel Model { get; set; }
        }

        private readonly Fixture _fixture;

        public AddingEntityToAndConfiguringModel(Fixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void ContainsSingleEntity()
        {
            Assert.True(_fixture.Model.EntityTypes.Count == 1);
        }

        [Fact]
        public void ContainsCorrectEntity()
        {
            Assert.True(_fixture.Model.EntityTypes[0].ClrType == typeof(EntityOne));
        }

        [Fact]
        public void ContainsCorrectProperties()
        {
            var properties = _fixture.Model.EntityTypes[0].GetProperties().ToList();
            Assert.True(properties.Any(x => x.Name == "Id"));
            Assert.True(properties.Any(x => x.Name == "NotIgnored"));
        }
    }
}