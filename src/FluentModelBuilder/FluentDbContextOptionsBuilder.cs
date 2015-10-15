using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;

namespace FluentModelBuilder
{
    public class FluentDbContextOptionsBuilder : DbContextOptionsBuilder
    {
        protected virtual DbContextOptionsBuilder OptionsBuilder { get; }

        public FluentDbContextOptionsBuilder(DbContextOptionsBuilder optionsBuilder)
        {
            OptionsBuilder = optionsBuilder;
        }

        public virtual FluentDbContextOptionsBuilder ModelSource(IBuilderExtension modelSource)
        {
            modelSource.ApplyServices(OptionsBuilder);
            return this;
        }

        public virtual FluentDbContextOptionsBuilder Entities(Action<EntitiesBuilder> builderAction = null)
        {
            return SetOption(x =>
            {
                builderAction?.Invoke(x.Entities);
            });
        }

        protected virtual FluentDbContextOptionsBuilder SetOption(Action<FluentModelBuilderExtension> setAction)
        {
            var extension = CloneExtension();
            setAction(extension);
            ((IDbContextOptionsBuilderInfrastructure)OptionsBuilder).AddOrUpdateExtension(extension);
            return this;
        }

        protected FluentModelBuilderExtension CloneExtension()
        {
            return new FluentModelBuilderExtension(OptionsBuilder.Options.GetExtension<FluentModelBuilderExtension>());
        }
    }
}