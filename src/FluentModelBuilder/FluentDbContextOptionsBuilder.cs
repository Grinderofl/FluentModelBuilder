using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;

namespace FluentModelBuilder
{
    public class FluentDbContextOptionsBuilder
    {
        public DbContextOptionsBuilder OptionsBuilder { get; }

        public FluentDbContextOptionsBuilder(DbContextOptionsBuilder optionsBuilder)
        {
            OptionsBuilder = optionsBuilder;
        }

        public virtual FluentDbContextOptionsBuilder WithExtension(IBuilderExtension extension)
        {
            return SetOption(x => x.Extension = extension);
        }

        public virtual FluentDbContextOptionsBuilder Entities(Action<EntitiesBuilder> builderAction = null)
        {
            return SetOption(x =>
            {
                builderAction?.Invoke(x.Entities);
            });
        }

        public virtual FluentDbContextOptionsBuilder Overrides(Action<OverridesBuilder> builderAction = null)
        {
            return SetOption(x =>
            {
                builderAction?.Invoke(x.Overrides);
            });
        }

        protected virtual FluentDbContextOptionsBuilder SetOption(Action<FluentModelBuilderExtension> setAction)
        {
            var extension = CloneExtension();
            setAction(extension);
            ((IDbContextOptionsBuilderInfrastructure)OptionsBuilder).AddOrUpdateExtension(extension);
            return new FluentDbContextOptionsBuilder(OptionsBuilder);
        }

        protected FluentModelBuilderExtension CloneExtension()
        {
            return new FluentModelBuilderExtension(OptionsBuilder.Options.GetExtension<FluentModelBuilderExtension>());
        }
    }
}