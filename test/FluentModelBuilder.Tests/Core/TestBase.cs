using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Xunit;

namespace FluentModelBuilder.Tests.Core
{
    public abstract class TestBase<TFixture, TContext> : IClassFixture<TFixture>
        where TFixture : ModelFixtureBase<TContext> where TContext : DbContext
    {
        protected IEnumerable<IEntityType> EntityTypes;

        protected TestBase(TFixture fixture)
        {
            EntityTypes = fixture.Model.GetEntityTypes().OrderBy(x => x.Name);
        }

        protected IEnumerable<IProperty> GetProperties(int elementIndex)
        {
            return EntityTypes.ElementAt(elementIndex).GetProperties().OrderBy(x => x.Name);
        }
    }
}