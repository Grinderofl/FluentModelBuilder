using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentModelBuilder.Contributors.Internal.Criteria;
using FluentModelBuilder.Extensions;
using Microsoft.Data.Entity;

namespace FluentModelBuilder.Contributors.Internal
{
    public class DiscoveryOverrideContributor : IOverrideContributor
    {
        public IList<Assembly> Assemblies { get; set; } = new List<Assembly>();
        public IList<ITypeInfoCriterion> Criteria { get; set; } = new List<ITypeInfoCriterion>();

        protected AssembliesBuilder AssembliesBuilder;

        public DiscoveryOverrideContributor(AssembliesBuilder builder)
        {
            AssembliesBuilder = builder;
        }

        public DiscoveryOverrideContributor()
        {}

        public DiscoveryOverrideContributor AddAssembly(Assembly assembly)
        {
            if(!Assemblies.Contains(assembly))
                Assemblies.Add(assembly);
            return this;
        }

        public DiscoveryOverrideContributor AddCriterion(ITypeInfoCriterion criterion)
        {
            if(!Criteria.Contains(criterion))
                Criteria.Add(criterion);

            return this;
        }

        public DiscoveryOverrideContributor WithCriterion<T>(Action<T> criterionAction = null) where T : ITypeInfoCriterion
        {
            T criterion = (T) Criteria.FirstOrDefault(x => x is T);
            if (criterion == null)
            {
                criterion = Activator.CreateInstance<T>();
                AddCriterion(criterion);
            }
            criterionAction?.Invoke(criterion);
            return this;
        }

        protected virtual IEnumerable<Assembly> GetAssemblies()
        {
            return AssembliesBuilder?.Assemblies.Union(Assemblies) ?? Assemblies;
        }

        public void Contribute(ModelBuilder modelBuilder)
        {
            var types = GetAssemblies().Distinct().SelectMany(x => x.GetExportedTypes());
            var overrideTypes = types.Where(x => x.ImplementsInterfaceOfType(typeof(IEntityTypeOverride<>)));
            var criteriaTypes = overrideTypes.Where(x => Criteria.All(c => c.IsSatisfiedBy(x.GetTypeInfo())));

            //var types =
            //    GetAssemblies()
            //        .Distinct()
            //        .SelectMany(x => x.GetExportedTypes())
            //        .Where(x => typeof(IEntityTypeOverride<>).IsAssignableFrom(x))
            //        .Where(x => Criteria.All(c => c.IsSatisfiedBy(x.GetTypeInfo())));

            foreach (var type in criteriaTypes)
            {
                var contributor = new SingleOverrideContributor(type);
                contributor.Contribute(modelBuilder);
            }
        }
    }
}