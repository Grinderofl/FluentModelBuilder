using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentModelBuilder.Contributors.Internal.Criteria;
using Microsoft.Data.Entity;

namespace FluentModelBuilder.Contributors.Internal
{
    public abstract class DiscoveryContributorBase<T> where T : DiscoveryContributorBase<T>
    {
        protected AssembliesBuilder AssembliesBuilder;

        protected DiscoveryContributorBase(AssembliesBuilder builder)
        {
            AssembliesBuilder = builder;
        }

        protected DiscoveryContributorBase()
        {}

        public IList<Assembly> Assemblies { get; set; } = new List<Assembly>();
        public IList<ITypeInfoCriterion> Criteria { get; set; } = new List<ITypeInfoCriterion>();

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

        public abstract void Contribute(ModelBuilder modelBuilder);

        protected virtual IEnumerable<Assembly> GetAssemblies()
        {
            return AssembliesBuilder?.Assemblies.Union(Assemblies) ?? Assemblies;
        }
    }
}