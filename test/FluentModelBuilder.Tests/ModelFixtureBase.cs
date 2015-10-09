using FluentModelBuilder.Options;
using Microsoft.Data.Entity.Metadata;

namespace FluentModelBuilder.Tests
{
    public abstract class ModelFixtureBase
    {
        protected ModelFixtureBase()
        {
            var options = new FluentModelBuilderOptions();
            ConfigureOptions(options);
            Model = new FluentModelBuilder(options).Build();
        }

        protected abstract void ConfigureOptions(FluentModelBuilderOptions options);

        public IModel Model { get; }
    }
}