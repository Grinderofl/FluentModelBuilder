using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentModelBuilder.Conventions.Overrides;
using FluentModelBuilder.Extensions;
using FluentModelBuilder.Options;
using FluentModelBuilder.TestTarget;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Builders;
using Xunit;

namespace FluentModelBuilder.Tests
{
    public class AddingMultipleOverrideToModel : IClassFixture<AddingMultipleOverrideToModel.Fixture>
    {
        public class Fixture
        {
            public Fixture()
            {
                var options = new FluentModelBuilderOptions();
                options.AddOverride<EntityOneOverride>();
                options.AddOverride<EntityOneSecondOverride>();
                Model = new FluentModelBuilder(options).Build();
            }

            public IModel Model { get; set; }
        }

        public class EntityOneSecondOverride : IEntityTypeOverride<EntityOne>
        {
            public void Configure(EntityTypeBuilder<EntityOne> mapping)
            {
                mapping.Ignore(x => x.NotIgnored);
            }
        }

        public AddingMultipleOverrideToModel(Fixture fixture)
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
        }

        [Fact]
        public void DoesNotContainIgnoredProperty()
        {
            Assert.False(_model.EntityTypes[0].GetProperties().Any(x => x.Name == "IgnoredInOverride"));
            Assert.False(_model.EntityTypes[0].GetProperties().Any(x => x.Name == "NotIgnored"));
        }

    }
}
