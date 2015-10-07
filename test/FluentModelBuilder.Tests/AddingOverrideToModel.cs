using System.Linq;
using FluentModelBuilder.Extensions;
using FluentModelBuilder.Options;
using FluentModelBuilder.TestTarget;
using Microsoft.Data.Entity.Metadata;
using Xunit;

namespace FluentModelBuilder.Tests
{
    public class AddingSingleOverrideToModel : IClassFixture<AddingSingleOverrideToModel.Fixture>
    {
        public class Fixture
        {
            public Fixture()
            {
                var options = new FluentModelBuilderOptions();
                options.AddOverride<EntityOneOverride>();
                Model = new FluentModelBuilder(options).Build();
            }

            public IModel Model { get; set; }
        }

        public AddingSingleOverrideToModel(Fixture fixture)
        {
            _model = fixture.Model;
        }

        private readonly IModel _model;

        [Fact]
        public void ContainsEntity()
        {
            Assert.True(_model.EntityTypes.Count == 1);
        }

        [Fact]
        public void ContainsCorrectEntity()
        {
            Assert.True(_model.EntityTypes[0].ClrType == typeof(EntityOne));
        }

        [Fact]
        public void ContainsCorrectProperties()
        {
            var properties = _model.EntityTypes[0].GetProperties().ToList();
            Assert.True(properties.Any(x => x.Name == "Id"));
            Assert.True(properties.Any(x => x.Name == "NotIgnored"));
        }

        [Fact]
        public void DoesNotContainIgnoredProperty()
        {
            Assert.False(_model.EntityTypes[0].GetProperties().Any(x => x.Name == "IgnoredInOverride"));
        }

    }


}