using System;
using System.Collections.Generic;
using System.Reflection;
using FluentModelBuilder.Builder;
using FluentModelBuilder.Builder.Sources;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace FluentModelBuilder.Configuration
{
    public class FluentModelBuilderConfiguration
    {
        private readonly IList<AutoModelBuilder> _builders = new List<AutoModelBuilder>();

        public ConventionSetAlterationCollection Alterations = new ConventionSetAlterationCollection();

        /// <summary>
        ///     Map classes using provided type source
        /// </summary>
        /// <param name="source">Type source to use</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder Using(ITypeSource source) => AddAndConfigureFrom(From.Source(source));

        /// <summary>
        ///     Map classes using provided type source with supplied configuration
        /// </summary>
        /// <param name="source">Type source to use</param>
        /// <param name="configuration">Configuration to use</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder Using(ITypeSource source, IEntityAutoConfiguration configuration)
            => AddAndConfigureFrom(From.Source(source, configuration));

        /// <summary>
        ///     Map classes using provided type source with supplied expression
        /// </summary>
        /// <param name="source">Type source to use</param>
        /// <param name="expression">Configuration to use</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder Using(ITypeSource source, Func<Type, bool> expression)
            => AddAndConfigureFrom(From.Source(source, expression));

        /// <summary>
        ///     Map classes based on all manual specifications
        /// </summary>
        /// <remarks>
        ///     You would use this if you didn't want to use any assembly or source by default, but rather
        ///     manually configure entire AutoModelBuilder yourself.
        /// </remarks>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder Using() => AddAndConfigureFrom(From.Empty());

        /// <summary>
        ///     Map classes from provided assemblies
        /// </summary>
        /// <param name="assemblies">Assemblies to scan</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder Using(params Assembly[] assemblies) => AddAndConfigureFrom(From.Assemblies(assemblies));

        /// <summary>
        ///     Map classes from provided assemblies
        /// </summary>
        /// <param name="configuration">Configuration to use</param>
        /// <param name="assemblies">Assemblies to scan</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder Using(IEntityAutoConfiguration configuration, params Assembly[] assemblies)
            => AddAndConfigureFrom(From.Assemblies(configuration, assemblies));

        /// <summary>
        ///     Map classes based on all manual specifications with supplied expression
        /// </summary>
        /// <param name="expression"></param>
        public AutoModelBuilder Using(Func<Type, bool> expression)
            => AddAndConfigureFrom(From.Expression(expression));

        /// <summary>
        ///     Map classes using the assembly containing
        ///     <typeparam name="T"></typeparam>
        /// </summary>
        /// <typeparam name="T">Type contained in the required assembly</typeparam>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder UsingAssemblyOf<T>() => AddAndConfigureFrom(From.AssemblyOf<T>());

        /// <summary>
        ///     Map classes using the assembly containing
        ///     <typeparam name="T"></typeparam>
        ///     with supplied configuration
        /// </summary>
        /// <typeparam name="T">Type contained in the required assembly</typeparam>
        /// <param name="configuration">Configuration to use</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder UsingAssemblyOf<T>(IEntityAutoConfiguration configuration)
            => AddAndConfigureFrom(From.AssemblyOf<T>(configuration));

        /// <summary>
        ///     Map classes using the assembly containing
        ///     <typeparam name="T"></typeparam>
        ///     with supplied expression
        /// </summary>
        /// <typeparam name="T">Type contained in the required assembly</typeparam>
        /// <param name="expression">Expression to use</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder UsingAssemblyOf<T>(Func<Type, bool> expression)
            => AddAndConfigureFrom(From.AssemblyOf<T>(expression));

        /// <summary>
        ///     Map classes using the assembly containing the provided type
        /// </summary>
        /// <param name="type">Type contained in the required assembly</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder UsingAssemblyOf(Type type)
            => AddAndConfigureFrom(From.Assembly(type.GetTypeInfo().Assembly));

        /// <summary>
        ///     Map classes using the assembly containing the provided type
        /// </summary>
        /// <param name="type">Type contained in the required assembly</param>
        /// <param name="configuration">Configuration to use</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder UsingAssemblyOf(Type type, IEntityAutoConfiguration configuration)
            => AddAndConfigureFrom(From.Assembly(type.GetTypeInfo().Assembly, configuration));

        /// <summary>
        ///     Map classes using the assembly containing the provided type
        ///     with supplied expression
        /// </summary>
        /// <param name="type">Type contained in the required assembly</param>
        /// <param name="expression">Expression to use</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder UsingAssemblyOf(Type type, Func<Type, bool> expression)
            => AddAndConfigureFrom(From.Assembly(type.GetTypeInfo().Assembly, expression));

        private AutoModelBuilder AddAndConfigureFrom(AutoModelBuilder from)
        {
            _builders.Add(from);
            return from;
        }

        public FluentModelBuilderConfiguration Add(AutoModelBuilder builder)
        {
            _builders.Add(builder);
            return this;
        }

        public FluentModelBuilderConfiguration Add(Func<AutoModelBuilder> builder)
        {
            _builders.Add(builder());
            return this;
        }

        internal void Apply(BuilderContext parameters)
        {
            foreach (var b in _builders)
                b.Apply(parameters);
        }

        internal void Apply(ConventionSet conventionSet)
        {
            foreach (var alteration in Alterations)
                alteration.Alter(conventionSet);
        }
    }
}