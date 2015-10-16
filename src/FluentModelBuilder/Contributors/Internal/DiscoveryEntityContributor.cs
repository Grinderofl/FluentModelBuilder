using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentModelBuilder.Contributors.Internal.Criteria;
using Microsoft.Data.Entity;

namespace FluentModelBuilder.Contributors.Internal
{
    public class DiscoveryEntityContributor : IEntityContributor
    {
        public IList<Assembly> Assemblies { get; set; } = new List<Assembly>();
        public IList<ITypeInfoCriterion> Criteria { get; set; } = new List<ITypeInfoCriterion>();

        public DiscoveryEntityContributor AddAssembly(Assembly assembly)
        {
            if(!Assemblies.Contains(assembly))
                Assemblies.Add(assembly);
            return this;
        }

        public DiscoveryEntityContributor AddCriterion(ITypeInfoCriterion criterion)
        {
            if(!Criteria.Contains(criterion))
                Criteria.Add(criterion);

            return this;
        }

        public DiscoveryEntityContributor WithCriterion<T>(Action<T> criterionAction = null) where T : ITypeInfoCriterion
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

        public void Contribute(ModelBuilder modelBuilder)
        {
            var types =
                Assemblies.SelectMany(x => x.GetExportedTypes())
                    .Where(x => Criteria.All(c => c.IsSatisfiedBy(x.GetTypeInfo())));

            foreach (var type in types)
                modelBuilder.Entity(type);
        }
    }
}