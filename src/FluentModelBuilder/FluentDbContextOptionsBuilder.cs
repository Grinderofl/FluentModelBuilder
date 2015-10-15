using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;

namespace FluentModelBuilder
{
    public class FluentDbContextOptionsBuilder
    {
        protected virtual DbContextOptionsBuilder OptionsBuilder { get; }

        public FluentDbContextOptionsBuilder(DbContextOptionsBuilder optionsBuilder)
        {
            OptionsBuilder = optionsBuilder;
        }

        public virtual FluentDbContextOptionsBuilder ModelSource(IModelSourceBuilder modelSource)
        {
            return SetOption(x => x.ModelSourceBuilder = modelSource);
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