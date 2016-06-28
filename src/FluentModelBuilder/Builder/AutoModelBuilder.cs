using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentModelBuilder.Alterations;
using FluentModelBuilder.Builder.Sources;
using FluentModelBuilder.Configuration;
using FluentModelBuilder.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;

namespace FluentModelBuilder.Builder
{
    public class AutoModelBuilder
    {
        private readonly IList<InlineOverride> _inlineOverrides = new List<InlineOverride>();
        private readonly IList<IModelBuilderOverride> _modelBuilderOverrides = new List<IModelBuilderOverride>();

        private readonly List<ITypeSource> _typeSources = new List<ITypeSource>();

        private readonly List<Type> _includedTypes = new List<Type>();
        private readonly List<Type> _ignoredTypes = new List<Type>();

        private readonly List<Func<DbContext, bool>> _dbContextSelectors = new List<Func<DbContext, bool>>();

        private readonly AutoModelBuilderAlterationCollection _alterations = new AutoModelBuilderAlterationCollection();

        public readonly IEntityAutoConfiguration Configuration;

        private BuilderScope? _scope;
        private Func<Type, bool> _whereClause;
        private readonly AutoConfigurationExpressions _expressions = new AutoConfigurationExpressions();

        public AutoModelBuilder() : this(new AutoConfigurationExpressions())
        {
        }

        public AutoModelBuilder(AutoConfigurationExpressions expressions) : this(new ExpressionBasedEntityAutoConfiguration(expressions))
        {
            _expressions = expressions;
        }
        
        public AutoModelBuilder(IEntityAutoConfiguration configuration)
        {
            if(configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            Configuration = configuration;
            Scope = new ScopeBuilder(this);
        }

        #region Where

        protected bool HasUserDefinedConfiguration => !(Configuration is ExpressionBasedEntityAutoConfiguration);

        /// <summary>
        /// Alter the Expression based configuration options, when not wanting to use user-defined configuration.
        /// </summary>
        /// <param name="configurationAction">Action to perform on Expression Configuration</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder Setup(Action<AutoConfigurationExpressions> configurationAction)
        {
            if(HasUserDefinedConfiguration)
                throw new InvalidOperationException("Cannot use Setup method when using user-defined IEntityAutoConfiguration instance.");
            configurationAction(_expressions);
            return this;
        }

        /// <summary>
        /// Specify a criteria to determine which types will be mapped. Cannot be used with user-defined configuration.
        /// </summary>
        /// <param name="where">Criteria for determining types</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder Where(Func<Type, bool> where)
        {
            if(HasUserDefinedConfiguration)
                throw new InvalidOperationException("Cannot use Where method when using user-defined IEntityAutoConfiguration instance.");
            _whereClause = where;
            return this;
        }

        #endregion

        #region Scope

        /// <summary>
        /// Specify the scope of this AutoModelBuilder, default is PreModelCreating
        /// <remarks>
        /// You would use this to change when the changes should be applied to the model - whether PreModelCreating,
        /// i.e. before Entity Framework's own model creation, or PostModelCreating, i.e. after Entity Framework
        /// has already created its own model. This is useful for things like overriding the base properties
        /// of IdentityDbContext entities, like UserName, Email, etc.
        /// </remarks>
        /// </summary>
        public ScopeBuilder Scope { get; }

        /// <summary>
        /// Specify the scope of this AutoModelBuilder, default is PreModelCreating
        /// <remarks>
        /// You would use this to change when the changes should be applied to the model - whether PreModelCreating,
        /// i.e. before Entity Framework's own model creation, or PostModelCreating, i.e. after Entity Framework
        /// has already created its own model. This is useful for things like overriding the base properties
        /// of IdentityDbContext entities, like UserName, Email, etc.
        /// </remarks>
        /// </summary>
        /// <param name="scope">Scope to use</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder UseScope(BuilderScope? scope)
        {
            _scope = scope;
            return this;
        }

        #endregion


        #region Context

        /// <summary>
        /// Add a DbContext selector to be used to determine whether this AutoModelBuilder should be used
        /// with the provided DbContext
        /// </summary>
        /// <typeparam name="TContext">DbContext type to use</typeparam>
        /// <param name="predicate">Predicate to match DbContext on</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder Context<TContext>(Func<TContext, bool> predicate = null) where TContext : DbContext
        {
            if (predicate == null)
                _dbContextSelectors.Add(x => x.GetType() == typeof (TContext));
            else
                _dbContextSelectors.Add(predicate as Func<DbContext, bool>);
            return this;
        }

        /// <summary>
        /// Add a DbContext selector to be used to determine whether this AutoModelBuilder should be used
        /// with the provided DbContext
        /// </summary>
        /// <param name="predicate">Predicate to match DbContext on</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder Context(Func<DbContext, bool> predicate)
        {
            return Context<DbContext>(predicate);
        }

        #endregion
        

        #region Alterations

        /// <summary>
        /// Configures alterations to be used with this AutoModelBuilder
        /// </summary>
        /// <param name="alterationDelegate">Action delegate for alteration</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder Alterations(Action<AutoModelBuilderAlterationCollection> alterationDelegate)
        {
            alterationDelegate(_alterations);
            return this;
        }

        /// <summary>
        /// Adds an alteration to be used with this AutoModelBuilder
        /// </summary>
        /// <typeparam name="TAlteration">Alteration to use</typeparam>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder AddAlteration<TAlteration>() where TAlteration : IAutoModelBuilderAlteration
        {
            return AddAlteration(typeof (TAlteration));
        }

        /// <summary>
        /// Adds an alteration to be used with this AutoModelBuilder
        /// </summary>
        /// <param name="type">Type of Alteration to use. Expects to be IAutoModelBuilderAlteration</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder AddAlteration(Type type)
        {
            if(!type.ClosesInterface(typeof(IAutoModelBuilderAlteration)))
                throw new ArgumentException($"Type does not implement interface {nameof(IAutoModelBuilderAlteration)}", nameof(type));
            return
                Alterations(
                    a => a.Add(ActivatorUtilities.CreateInstance(null, type, null) as IAutoModelBuilderAlteration));
        }

        /// <summary>
        /// Adds an alteration to be used with this AutoModelBuilder
        /// </summary>
        /// <param name="alteration">Alteration to use</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder AddAlteration(IAutoModelBuilderAlteration alteration)
        {
            return Alterations(a => a.Add(alteration));
        }

        #endregion


        #region Overrides

        /// <summary>
        /// Add mapping overrides defined in assembly of T
        /// </summary>
        /// <typeparam name="T">Type contained in required assembly</typeparam>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder UseOverridesFromAssemblyOf<T>()
        {
            return UseOverridesFromAssembly(typeof (T).GetTypeInfo().Assembly);
        }

        /// <summary>
        /// Add mapping overrides from specified assembly
        /// </summary>
        /// <param name="assembly">Assembly to use</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder UseOverridesFromAssembly(Assembly assembly)
        {
            _alterations.Add(new EntityTypeOverrideAlteration(assembly));
            _alterations.Add(new ModelBuilderOverrideAlteration(assembly));
            return this;
        }

        /// <summary>
        /// Adds an IEntityTypeOverride via reflection
        /// </summary>
        /// <param name="overrideType">Type of override, expected to be IEntityTypeOverride</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder Override(Type overrideType)
        {
            var overrideMethod = typeof(AutoModelBuilder)
                .GetMethod(nameof(OverrideHelper), BindingFlags.NonPublic | BindingFlags.Instance);
            if (overrideMethod == null)
                return this;

            var overrideInterfaces = overrideType.GetInterfaces().Where(x => x.IsEntityTypeOverrideType()).ToList();
            var overrideInstance = Activator.CreateInstance(overrideType);

            foreach (var overrideInterface in overrideInterfaces)
            {
                var entityType = overrideInterface.GetGenericArguments().First();
                AddOverride(entityType, instance =>
                {
                    overrideMethod.MakeGenericMethod(entityType)
                        .Invoke(this, new[] { instance, overrideInstance });
                });

            }
            return this;
        }

        /// <summary>
        /// Override the mapping of specific entity
        /// <remarks>
        /// Can also be used to add single entities that are not picked up via assembly scanning
        /// </remarks>
        /// </summary>
        /// <typeparam name="T">Type of entity to override</typeparam>
        /// <param name="builderAction">Action to perform override</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder Override<T>(Action<EntityTypeBuilder<T>> builderAction = null) where T : class
        {
            _inlineOverrides.Add(new InlineOverride(typeof(T), x =>
            {
                if (x is EntityTypeBuilder<T>)
                    builderAction?.Invoke((EntityTypeBuilder<T>)x);
            }));
            return this;
        }

        /// <summary>
        /// Override the ModelBuilder
        /// </summary>
        /// <param name="modelBuilderOverride">Type of IModelBuilderOverride</param>
        public void Override(IModelBuilderOverride modelBuilderOverride)
        {
            _modelBuilderOverrides.Add(modelBuilderOverride);
        }

        #endregion


        #region Entities

        /// <summary>
        /// Adds entities from specific assembly
        /// </summary>
        /// <param name="assembly">Assembly to use</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder AddEntityAssembly(Assembly assembly)
        {
            return AddTypeSource(new AssemblyTypeSource(assembly));
        }

        /// <summary>
        /// Adds entities from specific assembly
        /// </summary>
        /// <typeparam name="T">Type contained in required assembly</typeparam>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder AddEntityAssemblyOf<T>()
        {
            return AddEntityAssembly(typeof(T).GetTypeInfo().Assembly);
        }

        /// <summary>
        /// Adds entities from specific assembly
        /// </summary>
        /// <param name="type">Type contained in required assembly</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder AddEntityAssemblyOf(Type type)
        {
            return AddEntityAssembly(type.GetTypeInfo().Assembly);
        }

        /// <summary>
        /// Explicitly includes a type to be used as part of the model
        /// </summary>
        /// <typeparam name="T">Type to include</typeparam>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder IncludeBase<T>()
        {
            return IncludeBase(typeof(T));
        }

        /// <summary>
        /// Explicitly includes a type to be used as part of the model
        /// </summary>
        /// <param name="type">Type to include</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder IncludeBase(Type type)
        {
            _includedTypes.Add(type);
            return this;
        }

        /// <summary>
        /// Ignores a type and ensures it will not be used as part of the model
        /// </summary>
        /// <remarks>
        /// In the event that you wish to ignore an entity that would be otherwise be picked up due to <see cref="IEntityAutoConfiguration"/>,
        /// you would want to use this method
        /// </remarks>
        /// <typeparam name="T">Type to ignore</typeparam>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder IgnoreBase<T>()
        {
            return IgnoreBase(typeof(T));
        }

        /// <summary>
        /// Ignores a type and ensures it will not be used as part of the model
        /// </summary>
        /// <remarks>
        /// In the event that you wish to ignore an entity that would be otherwise be picked up due to <see cref="IEntityAutoConfiguration"/>,
        /// you would want to use this method
        /// </remarks>
        /// <param name="type">Type to ignore</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder IgnoreBase(Type type)
        {
            _ignoredTypes.Add(type);
            return this;
        }

#endregion

        /// <summary>
        /// Adds entities from the <see cref="ITypeSource"/>
        /// </summary>
        /// <param name="typeSource"><see cref="ITypeSource"/> to use</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder AddTypeSource(ITypeSource typeSource)
        {
            _typeSources.Add(typeSource);
            return this;
        }

        internal void Apply(BuilderContext parameters)
        {
            if (!ShouldApplyToContext(parameters.DbContext))
                return;

            if (!ShouldApplyToScope(parameters.Scope))
                return;

            _alterations.Apply(this);
            AddEntities(parameters.ModelBuilder);
            ApplyOverrides(parameters.ModelBuilder);
        }

        internal void AddOverride(Type type, Action<object> action)
        {
            _inlineOverrides.Add(new InlineOverride(type, action));
        }

        private void OverrideHelper<T>(EntityTypeBuilder<T> builder, IEntityTypeOverride<T> mappingOverride) where T : class
        {
            mappingOverride.Override(builder);
        }

        private object EntityTypeBuilder(ModelBuilder builder, Type type)
        {
            var entityMethod =
                typeof (ModelBuilder).GetMethods(BindingFlags.Public | BindingFlags.Instance)
                    .FirstOrDefault(x => x.Name == "Entity" && x.IsGenericMethod);

            var genericEntityMethod = entityMethod?.MakeGenericMethod(type);
            return genericEntityMethod?.Invoke(builder, null);
        }

        

        private void AddEntities(ModelBuilder builder)
        {
            var types = _typeSources.SelectMany(x => x.GetTypes()).Distinct();
            foreach (var type in types)
            {
                if (!Configuration.ShouldMap(type))
                    continue;

                if (_whereClause != null && !_whereClause(type))
                    continue;

                if (!ShouldMap(type))
                    continue;

                builder.Entity(type);
            }
        }

        private void ApplyOverrides(ModelBuilder builder)
        {
            foreach (var inlineOverride in _inlineOverrides)
            {
                var entityTypeBuilderInstance = EntityTypeBuilder(builder, inlineOverride.Type);
                inlineOverride.Apply(entityTypeBuilderInstance);
            }
        }

        private bool ShouldApplyToContext(DbContext dbContext)
        {
            if (!Configuration.ShouldApplyToContext(dbContext))
                return false;
            foreach (var selector in _dbContextSelectors)
            {
                if (!selector(dbContext))
                    return false;
            }
            return true;
        }

        private bool ShouldApplyToScope(BuilderScope scope)
        {
            if (!Configuration.ShouldApplyToScope(scope))
                return false;
            if (_scope == null)
                return scope == BuilderScope.PostModelCreating;
            if (_scope != scope)
                return false;

            return true;
        }

        private bool ShouldMap(Type type)
        {
            if (_includedTypes.Contains(type))
                return true;
            if (_ignoredTypes.Contains(type))
                return false;
            if (type.GetTypeInfo().IsGenericType && _ignoredTypes.Contains(type.GetGenericTypeDefinition()))
                return false;
            if (type.GetTypeInfo().IsAbstract)
                return false;

            if (type == typeof (object))
                return false;

            return true;
        }
    }
}