using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace FluentModelBuilder.Configuration
{
    public class FluentModelBuilderOptionsBuilder
    {
        private readonly DbContextOptionsBuilder _builder;

        public FluentModelBuilderOptionsBuilder(DbContextOptionsBuilder builder)
        {
            _builder = builder;
        }

        protected virtual FluentModelBuilderOptionsBuilder SetOption(
            Action<FluentModelBuilderOptionsExtension> setAction)
        {
            var extension = new FluentModelBuilderOptionsExtension();
            setAction(extension);
            ((IDbContextOptionsBuilderInfrastructure) _builder).AddOrUpdateExtension(extension);
            return this;
        }

        public virtual FluentModelBuilderOptionsBuilder Configuration(Action<FluentModelBuilderConfiguration> action)
            => SetOption(x => x.Configure(action));
    }
}