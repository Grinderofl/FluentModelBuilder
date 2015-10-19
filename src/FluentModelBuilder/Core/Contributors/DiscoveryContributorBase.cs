using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentModelBuilder.Core.Criteria;
using Microsoft.Data.Entity;

namespace FluentModelBuilder.Core.Contributors
{
    public abstract class DiscoveryContributorBase
    {
        protected AssembliesBuilder AssembliesBuilder { get; private set; }
        public bool UseSharedAssemblies { get; set; }
        public IList<Assembly> Assemblies { get; set; } = new List<Assembly>();
        public IList<ITypeInfoCriterion> Criteria { get; set; } = new List<ITypeInfoCriterion>();

        internal void SetAssembliesBuilder(AssembliesBuilder builder)
        {
            AssembliesBuilder = builder;
        }

        public void Contribute(ModelBuilder modelBuilder)
        {
            ContributeCore(modelBuilder);
        }

        protected abstract void ContributeCore(ModelBuilder modelBuilder);

        protected virtual IEnumerable<Assembly> GetAssemblies()
        {
            return AssembliesBuilder?.Assemblies.Union(Assemblies) ?? Assemblies;
        }
    }

    public abstract class DiscoveryContributorBase<T> : DiscoveryContributorBase where T : DiscoveryContributorBase<T>
    {
        public T FromSharedAssemblies(bool useSharedAssemblies = true)
        {
            UseSharedAssemblies = useSharedAssemblies;
            return (T) this;
        }

        public T AddAssembly(Assembly assembly)
        {
            if(!Assemblies.Contains(assembly))
                Assemblies.Add(assembly);
            return (T)this;
        }

        public T AddCriterion(ITypeInfoCriterion criterion)
        {
            if(!Criteria.Contains(criterion))
                Criteria.Add(criterion);

            return (T) this;
        }

        public T WithCriterion<T2>(Action<T2> criterionAction = null) where T2 : ITypeInfoCriterion
        {
            T2 criterion = (T2) Criteria.FirstOrDefault(x => x is T2);
            if (criterion == null)
            {
                criterion = Activator.CreateInstance<T2>();
                AddCriterion(criterion);
            }
            criterionAction?.Invoke(criterion);
            return (T)this;
        }
    }
}