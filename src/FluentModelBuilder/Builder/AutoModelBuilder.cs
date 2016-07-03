using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentModelBuilder.Alterations;
using FluentModelBuilder.Builder.Conventions;
using FluentModelBuilder.Builder.Sources;
using FluentModelBuilder.Configuration;
using FluentModelBuilder.Conventions;
using FluentModelBuilder.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;

namespace FluentModelBuilder.Builder
{
    public class AutoModelBuilder
    {
        private readonly List<Func<DbContext, bool>> _dbContextSelectors = new List<Func<DbContext, bool>>();
        private readonly AutoConfigurationExpressions _expressions = new AutoConfigurationExpressions();
        private readonly List<Type> _ignoredTypes = new List<Type>();

        private readonly List<Type> _includedTypes = new List<Type>();
        private readonly IList<InlineOverride> _inlineOverrides = new List<InlineOverride>();

        private readonly IList<IObjectFactory<IAutoModelBuilderAlteration>> _alterationFactories =
            new List<IObjectFactory<IAutoModelBuilderAlteration>>();

        private readonly IList<IObjectFactory<IModelBuilderConvention>> _conventionFactories =
            new List<IObjectFactory<IModelBuilderConvention>>();
        
        private readonly List<ITypeSource> _typeSources = new List<ITypeSource>();

        public readonly IEntityAutoConfiguration Configuration;

        private BuilderScope? _scope;
        private Func<Type, bool> _whereClause;
        private bool _alterationsApplied;

        public AutoModelBuilder() : this(new AutoConfigurationExpressions())
        {
        }

        public AutoModelBuilder(AutoConfigurationExpressions expressions)
            : this(new ExpressionBasedEntityAutoConfiguration(expressions))
        {
            _expressions = expressions;
        }

        public AutoModelBuilder(IEntityAutoConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            Configuration = configuration;
            Scope = new ScopeBuilder(this);
        }

        /// <summary>
        ///     Adds entities from the <see cref="ITypeSource" />
        /// </summary>
        /// <param name="typeSource"><see cref="ITypeSource" /> to use</param>
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

            var serviceProvider = parameters.DbContext.GetInfrastructure();

            ApplyAlterations(this, serviceProvider);
            AddEntities(parameters.ModelBuilder);
            ApplyModelBuilderOverrides(parameters.ModelBuilder, serviceProvider);
            ApplyOverrides(parameters.ModelBuilder);
        }

        internal void AddOverride(Type type, Action<object> action)
        {
            _inlineOverrides.Add(new InlineOverride(type, action));
        }

        private void OverrideHelper<T>(EntityTypeBuilder<T> builder, IEntityTypeOverride<T> mappingOverride)
            where T : class
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

        private void ApplyAlterations(AutoModelBuilder autoModelBuilder, IServiceProvider serviceProvider)
        {
            if (_alterationsApplied) return;
            foreach(var alteration in _alterationFactories.Select(x => x.Create(serviceProvider)))
                alteration.Alter(autoModelBuilder);
            _alterationsApplied = true;
        }

        private void ApplyModelBuilderOverrides(ModelBuilder builder, IServiceProvider serviceProvider)
        {
            foreach (var modelBuilderOverride in 
                    _conventionFactories.Select(x => x.Create(serviceProvider)))
                modelBuilderOverride.Override(builder);
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

        #region Where

        protected bool HasUserDefinedConfiguration => !(Configuration is ExpressionBasedEntityAutoConfiguration);

        /// <summary>
        ///     Alter the Expression based configuration options, when not wanting to use user-defined configuration.
        /// </summary>
        /// <param name="configurationAction">Action to perform on Expression Configuration</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder Setup(Action<AutoConfigurationExpressions> configurationAction)
        {
            if (HasUserDefinedConfiguration)
                throw new InvalidOperationException(
                    "Cannot use Setup method when using user-defined IEntityAutoConfiguration instance.");
            configurationAction(_expressions);
            return this;
        }

        /// <summary>
        ///     Specify a criteria to determine which types will be mapped. Cannot be used with user-defined configuration.
        /// </summary>
        /// <param name="where">Criteria for determining types</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder Where(Func<Type, bool> where)
        {
            if (HasUserDefinedConfiguration)
                throw new InvalidOperationException(
                    "Cannot use Where method when using user-defined IEntityAutoConfiguration instance.");
            _whereClause = where;
            return this;
        }

        #endregion

        #region Scope

        /// <summary>
        ///     Specify the scope of this AutoModelBuilder, default is PreModelCreating
        ///     <remarks>
        ///         You would use this to change when the changes should be applied to the model - whether PreModelCreating,
        ///         i.e. before Entity Framework's own model creation, or PostModelCreating, i.e. after Entity Framework
        ///         has already created its own model. This is useful for things like overriding the base properties
        ///         of IdentityDbContext entities, like UserName, Email, etc.
        ///     </remarks>
        /// </summary>
        public ScopeBuilder Scope { get; }

        /// <summary>
        ///     Specify the scope of this AutoModelBuilder, default is PreModelCreating
        ///     <remarks>
        ///         You would use this to change when the changes should be applied to the model - whether PreModelCreating,
        ///         i.e. before Entity Framework's own model creation, or PostModelCreating, i.e. after Entity Framework
        ///         has already created its own model. This is useful for things like overriding the base properties
        ///         of IdentityDbContext entities, like UserName, Email, etc.
        ///     </remarks>
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
        ///     Add a DbContext selector to be used to determine whether this AutoModelBuilder should be used
        ///     with the provided DbContext
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
        ///     Add a DbContext selector to be used to determine whether this AutoModelBuilder should be used
        ///     with the provided DbContext
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
        ///     Adds an alteration to be used with this AutoModelBuilder
        /// </summary>
        /// <typeparam name="TAlteration">Alteration to use</typeparam>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder AddAlteration<TAlteration>() where TAlteration : IAutoModelBuilderAlteration 
            => AddAlteration(typeof (TAlteration));

        /// <summary>
        ///     Adds an alteration to be used with this AutoModelBuilder
        /// </summary>
        /// <param name="type">Type of Alteration to use. Expects to be IAutoModelBuilderAlteration</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder AddAlteration(Type type)
        {
            if (!type.ClosesInterface(typeof (IAutoModelBuilderAlteration)))
                throw new ArgumentException($"Type does not implement interface {nameof(IAutoModelBuilderAlteration)}",
                    nameof(type));
            _alterationFactories.Add(new TypeBasedObjectFactory<IAutoModelBuilderAlteration>(type));
            return this;
        }

        /// <summary>
        ///     Adds an alteration to be used with this AutoModelBuilder
        /// </summary>
        /// <param name="alteration">Alteration to use</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder AddAlteration(IAutoModelBuilderAlteration alteration)
        {
            _alterationFactories.Add(new InstancedObjectFactory<IAutoModelBuilderAlteration>(alteration));
            return this;
        }

        /// <summary>
        ///     Adds all alterations from provided assembly to be used with this AutoModelBuilder
        /// </summary>
        /// <param name="assembly">Assembly to scan for alterations</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder AddAlterationsFromAssembly(Assembly assembly)
        {
            var types =
                assembly.GetTypesImplementingInterface(typeof(IAutoModelBuilderAlteration));
            foreach(var type in types)
                _alterationFactories.Add(new TypeBasedObjectFactory<IAutoModelBuilderAlteration>(type));
            return this;
        }

        /// <summary>
        ///     Adds all alterations from the assembly of provided type to be used with this AutoModelBuilder
        /// </summary>
        /// <typeparam name="T">Type contained in the assembly to be scanned</typeparam>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder AddAlterationsFromAssemblyOf<T>()
            => AddAlterationsFromAssemblyOf(typeof(T));

        /// <summary>
        ///     Adds all alterations from the assembly of provided type to be used with this AutoModelBuilder
        /// </summary>
        /// <param name="type">Type contained in the assembly to be scanned</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder AddAlterationsFromAssemblyOf(Type type)
            => AddAlterationsFromAssembly(type.GetTypeInfo().Assembly);

        /// <summary>
        ///     Adds all alterations from provided assemblies to be used with this AutoModelBuilder
        /// </summary>
        /// <param name="assemblies">Assemblies to scan for alterations</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder AddAlterationsFromAssemblies(IEnumerable<Assembly> assemblies)
        {
            foreach (var assembly in assemblies)
                AddAlterationsFromAssembly(assembly);
            return this;
        }

        #endregion

        #region Entities

        /// <summary>
        ///     Adds entities from specific assembly
        /// </summary>
        /// <param name="assembly">Assembly to use</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder AddEntitiesFromAssembly(Assembly assembly)
            => AddTypeSource(new AssemblyTypeSource(assembly));

        /// <summary>
        ///     Adds entities from specified assemblies
        /// </summary>
        /// <param name="assemblies">Assemblies to use</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder AddEntitiesFromAssemblies(IEnumerable<Assembly> assemblies)
        {
            AddTypeSource(new CombinedAssemblyTypeSource(assemblies));
            return this;
        }

        /// <summary>
        ///     Adds entities from specific assembly
        /// </summary>
        /// <typeparam name="T">Type contained in required assembly</typeparam>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder AddEntitiesFromAssemblyOf<T>()
            => AddEntitiesFromAssembly(typeof(T).GetTypeInfo().Assembly);

        /// <summary>
        ///     Adds entities from specific assembly
        /// </summary>
        /// <param name="type">Type contained in required assembly</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder AddEntitiesFromAssemblyOf(Type type)
            => AddEntitiesFromAssembly(type.GetTypeInfo().Assembly);

        /// <summary>
        ///     Explicitly includes a type to be used as part of the model
        /// </summary>
        /// <typeparam name="T">Type to include</typeparam>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder IncludeBase<T>()
            => IncludeBase(typeof(T));

        /// <summary>
        ///     Explicitly includes a type to be used as part of the model
        /// </summary>
        /// <param name="type">Type to include</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder IncludeBase(Type type)
        {
            _includedTypes.Add(type);
            return this;
        }

        /// <summary>
        ///     Ignores a type and ensures it will not be used as part of the model
        /// </summary>
        /// <remarks>
        ///     In the event that you wish to ignore an entity that would be otherwise be picked up due to
        ///     <see cref="IEntityAutoConfiguration" />,
        ///     you would want to use this method
        /// </remarks>
        /// <typeparam name="T">Type to ignore</typeparam>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder IgnoreBase<T>()
            => IgnoreBase(typeof(T));

        /// <summary>
        ///     Ignores a type and ensures it will not be used as part of the model
        /// </summary>
        /// <remarks>
        ///     In the event that you wish to ignore an entity that would be otherwise be picked up due to
        ///     <see cref="IEntityAutoConfiguration" />,
        ///     you would want to use this method
        /// </remarks>
        /// <param name="type">Type to ignore</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder IgnoreBase(Type type)
        {
            _ignoredTypes.Add(type);
            return this;
        }

        #endregion

        #region Conventions

        /// <summary>
        ///     Add a convention for the ModelBuilder
        /// </summary>
        /// <param name="modelBuilderConvention">Convention to add</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder UseConvention(IModelBuilderConvention modelBuilderConvention)
        {
            _conventionFactories.Add(new InstancedObjectFactory<IModelBuilderConvention>(modelBuilderConvention));
            //_modelBuilderConventions.Add(modelBuilderConvention);
            return this;
        }

        /// <summary>
        ///     Add a convention for the ModelBuilder
        /// </summary>
        /// <typeparam name="TConvention">Type of IModelBuilderConvention</typeparam>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder UseConvention<TConvention>() where TConvention : IModelBuilderConvention, new()
            => UseConvention(typeof(TConvention));

        /// <summary>
        ///     Add a convention for the ModelBuilder
        /// </summary>
        /// <param name="type">Type of to add. Expected to be IModelBuilderConvention</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder UseConvention(Type type)
        {
            _conventionFactories.Add(new TypeBasedObjectFactory<IModelBuilderConvention>(type));
            return this;
        }

        /// <summary>
        ///     Add conventions for the ModelBuilder
        /// </summary>
        /// <param name="modelBuilderConventions">Conventions to add</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder UseConventions(IEnumerable<IModelBuilderConvention> modelBuilderConventions)
        {
            foreach (var convention in modelBuilderConventions)
                _conventionFactories.Add(new InstancedObjectFactory<IModelBuilderConvention>(convention));
                //_modelBuilderConventions.Add(convention);
            return this;
        }

        /// <summary>
        ///     Add conventions from specified assembly
        /// </summary>
        /// <param name="assembly">Assembly to use</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder UseConventionsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetTypesImplementingInterface(typeof(IModelBuilderConvention));
            foreach(var type in types)
                _conventionFactories.Add(new TypeBasedObjectFactory<IModelBuilderConvention>(type));
            //_alterationFactories.Add(
            //    new InstancedObjectFactory<IAutoModelBuilderAlteration>(new ModelBuilderConventionAlteration(assembly)));
            //_alterations.Add(new ModelBuilderConventionAlteration(assembly));
            return this;
        }

        /// <summary>
        ///     Add conventions from specified assemblies
        /// </summary>
        /// <param name="assemblies">Assemblies to use</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder UseConventionsFromAssemblies(IEnumerable<Assembly> assemblies)
        {
            foreach (var assembly in assemblies)
                UseConventionsFromAssembly(assembly);
                //_alterations.Add(new ModelBuilderConventionAlteration(assembly));
            return this;
        }

        /// <summary>
        ///     Add conventions from assembly containing the specified type
        /// </summary>
        /// <param name="type">Type contained in the assembly to use</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder UseConventionsFromAssemblyOf(Type type)
            => UseConventionsFromAssembly(type.GetTypeInfo().Assembly);

        /// <summary>
        ///     Add conventions from assembly containing the specified type
        /// </summary>
        /// <typeparam name="T">Type contained in the assembly to use</typeparam>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder UseConventionsFromAssemblyOf<T>()
            => UseConventionsFromAssemblyOf(typeof(T));

        #endregion

        #region Overrides

        /// <summary>
        ///     Add mapping overrides from specified assembly
        /// </summary>
        /// <param name="assembly">Assembly to use</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder UseOverridesFromAssembly(Assembly assembly)
        {
            var types = assembly.GetTypesImplementingInterface(typeof(IEntityTypeOverride<>));
            foreach (var type in types)
                UseOverride(type);
            return this;
        }

        /// <summary>
        ///     Add mapping overrides from specified assemblies
        /// </summary>
        /// <param name="assemblies">Assemblies to use</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder UseOverridesFromAssemblies(IEnumerable<Assembly> assemblies)
        {
            foreach (var assembly in assemblies)
                UseOverridesFromAssembly(assembly);
            return this;
        }

        /// <summary>
        ///     Add mapping overrides defined in assembly of the provided type
        /// </summary>
        /// <param name="type">Type contained in the required assembly</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder UseOverridesFromAssemblyOf(Type type)
            => UseOverridesFromAssembly(type.GetTypeInfo().Assembly);

        /// <summary>
        ///     Add mapping overrides defined in assembly of T
        /// </summary>
        /// <typeparam name="T">Type contained in required assembly</typeparam>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder UseOverridesFromAssemblyOf<T>()
            => UseOverridesFromAssemblyOf(typeof (T));

        /// <summary>
        ///     Adds an IEntityTypeOverride via reflection
        /// </summary>
        /// <param name="overrideType">Type of override, expected to be IEntityTypeOverride</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder UseOverride(Type overrideType)
        {
            var overrideMethod = typeof (AutoModelBuilder)
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
                        .Invoke(this, new[] {instance, overrideInstance});
                });
            }
            return this;
        }

        /// <summary>
        ///     Override the mapping of specific entity
        ///     <remarks>
        ///         Can also be used to add single entities that are not picked up via assembly scanning
        ///     </remarks>
        /// </summary>
        /// <typeparam name="T">Type of entity to override</typeparam>
        /// <param name="builderAction">Action to perform override</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder Override<T>(Action<EntityTypeBuilder<T>> builderAction = null) where T : class
        {
            _inlineOverrides.Add(new InlineOverride(typeof (T), x =>
            {
                var builder = x as EntityTypeBuilder<T>;
                if (builder != null)
                    builderAction?.Invoke(builder);
            }));
            return this;
        }
        #endregion

        #region All

        /// <summary>
        ///     Adds Alterations, Entities, Conventions, and Overrides (in this order) from specified assembly
        /// </summary>
        /// <param name="assembly">Assembly to add from</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder AddFromAssembly(Assembly assembly)
        {
            AddAlterationsFromAssembly(assembly);
            AddEntitiesFromAssembly(assembly);
            UseConventionsFromAssembly(assembly);
            UseOverridesFromAssembly(assembly);
            return this;
        }

        /// <summary>
        ///     Adds Alterations, Entities, Conventions, and Overrides (in this order) from specified assemblies
        /// </summary>
        /// <param name="assemblies">Assemblies to add from</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder AddFromAssemblies(IEnumerable<Assembly> assemblies)
        {
            AddAlterationsFromAssemblies(assemblies);
            AddEntitiesFromAssemblies(assemblies);
            UseConventionsFromAssemblies(assemblies);
            UseOverridesFromAssemblies(assemblies);
            return this;
        }

        /// <summary>
        ///     Adds Alterations, Entities, Conventions, and Overrides (in this order) from assembly containing the specified type
        /// </summary>
        /// <param name="type">Type contained in the assembly to add from</param>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder AddFromAssemblyOf(Type type)
            => AddFromAssembly(type.GetTypeInfo().Assembly);

        /// <summary>
        ///     Adds Alterations, Entities, Conventions, and Overrides (in this order) from assembly containing the specified type
        /// </summary>
        /// <typeparam name="T">Type contained in the assembly to add from</typeparam>
        /// <returns>AutoModelBuilder</returns>
        public AutoModelBuilder AddFromAssemblyOf<T>()
            => AddFromAssemblyOf(typeof(T));

        #endregion


    }
}
