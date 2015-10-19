using System.Collections.Generic;
using System.Reflection;
using FluentModelBuilder.Contributors.Internal;

namespace FluentModelBuilder
{
    public class AssembliesBuilder
    {
        public IList<Assembly> Assemblies { get; set; } = new List<Assembly>();

        public AssembliesBuilder AddAssembly(Assembly assembly)
        {
            if(!Assemblies.Contains(assembly))
                Assemblies.Add(assembly);
            return this;
        }

        public AssembliesBuilder AddAssemblyContaining<T>()
        {
            return AddAssembly(typeof(T).GetTypeInfo().Assembly);
        }

        public void Apply<T>(T contributor) where T : DiscoveryContributorBase<T>
        {
            contributor.SetAssembliesBuilder(this);
        }
    }
}