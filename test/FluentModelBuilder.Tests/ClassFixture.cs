using Microsoft.Data.Entity.Metadata;
using Xunit;

namespace FluentModelBuilder.Tests
{
    public abstract class ClassFixture<T> : IClassFixture<T> where T : ModelFixtureBase
    {
        protected IModel Model;

        protected ClassFixture(T fixture)
        {
            Model = fixture.Model;
        }
    }
}