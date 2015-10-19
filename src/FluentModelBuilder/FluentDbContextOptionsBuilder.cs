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

        /// <summary>
        /// Configures the entities on the model for current DbContext
        /// </summary>
        /// <param name="builderAction">Action to perform on entities, e.g. add, configure, discover</param>
        /// <returns><see cref="FluentDbContextOptionsBuilder"/></returns>
        public virtual FluentDbContextOptionsBuilder Entities(Action<EntitiesBuilder> builderAction = null)
        {
            return SetOption(x =>
            {
                builderAction?.Invoke(x.Entities);
            });
        }

        /// <summary>
        /// Configures the IEntityTypeOverride`1[TEntity] for overriding entity mappings for current DbContext
        /// </summary>
        /// <param name="builderAction">Action to perform on overrides, e.g. add, configure, discover</param>
        /// <returns><see cref="FluentDbContextOptionsBuilder"/></returns>
        public virtual FluentDbContextOptionsBuilder Overrides(Action<OverridesBuilder> builderAction = null)
        {
            return SetOption(x =>
            {
                builderAction?.Invoke(x.Overrides);
            });
        }

        /// <summary>
        /// Configures the shared assemblies for Entities and Overrides 
        /// </summary>
        /// <param name="builderAction">Action to perform on assemblies, primarily to add.</param>
        /// <returns><see cref="FluentDbContextOptionsBuilder"/></returns>
        public virtual FluentDbContextOptionsBuilder Assemblies(Action<AssembliesBuilder> builderAction = null)
        {
            return SetOption(x =>
            {
                builderAction?.Invoke(x.Assemblies);
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