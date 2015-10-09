using System;
using System.Linq;
using FluentModelBuilder.Conventions;
using FluentModelBuilder.Conventions.Assemblies;
using FluentModelBuilder.Conventions.Assemblies.Options;
using FluentModelBuilder.Conventions.Entities;
using FluentModelBuilder.Conventions.Entities.Options;
using FluentModelBuilder.Conventions.Overrides;
using FluentModelBuilder.Conventions.Overrides.Options;
using FluentModelBuilder.Options;
using FluentModelBuilder.Sources;

namespace FluentModelBuilder.Extensions
{
    public static class FluentModelBuilderOptionsExtensions
    {

        /// <summary>
        /// Adds or alters an existing IModelBuilderConvention found in <see cref="FluentModelBuilderOptions"/>
        /// </summary>
        /// <typeparam name="T">Type of convention</typeparam>
        /// <param name="options"><see cref="FluentModelBuilderOptions"/></param>
        /// <returns>Instance of type IModelBuilderConvention</returns>
        public static T WithConvention<T>(this FluentModelBuilderOptions options) where T : class, IModelBuilderConvention, new()
        {
            var convention = options.Conventions.FirstOrDefault(x => x is T) as T;
            if (convention == null)
            {
                convention = new T();
                options.Conventions.Add(convention);
            }

            return convention;
        }

        /// <summary>
        /// Configures Entity Convention
        /// </summary>
        /// <param name="options"><see cref="FluentModelBuilderOptions"/></param>
        /// <param name="optionsAction">Actions to perform on Entity Convention</param>
        /// <returns><see cref="FluentModelBuilderOptions"/></returns>
        public static FluentModelBuilderOptions Entities(this FluentModelBuilderOptions options,
            Action<EntityConventionOptions> optionsAction = null)
        {
            var convention = options.WithConvention<EntityConvention>();
            optionsAction?.Invoke(convention.Options);
            return options;
        }

        /// <summary>
        /// Configures Assembly Convention
        /// </summary>
        /// <param name="options"><see cref="FluentModelBuilderOptions"/></param>
        /// <param name="optionsAction">Actions to perform on Assembly Convention</param>
        /// <returns><see cref="FluentModelBuilderOptions"/></returns>
        public static FluentModelBuilderOptions Assemblies(this FluentModelBuilderOptions options,
            Action<AssemblyConventionOptions> optionsAction = null)
        {
            var convention = options.WithConvention<AssemblyConvention>();
            optionsAction?.Invoke(convention.Options);
            return options;
        }

        /// <summary>
        /// Configures Override Convention
        /// </summary>
        /// <param name="options"><see cref="FluentModelBuilderOptions"/></param>
        /// <param name="optionsAction">Actions to perform on Override Convention</param>
        /// <returns><see cref="FluentModelBuilderOptions"/></returns>
        public static FluentModelBuilderOptions Overrides(this FluentModelBuilderOptions options,
            Action<OverrideConventionOptions> optionsAction = null)
        {
            var convention = options.WithConvention<OverrideConvention>();
            optionsAction?.Invoke(convention.Options);
            return options;
        }
    }
}