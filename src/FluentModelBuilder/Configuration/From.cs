using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentModelBuilder.Builder;
using FluentModelBuilder.Builder.Sources;

namespace FluentModelBuilder.Configuration
{
    public static class From
    {
        public static AutoModelBuilder Source(ITypeSource source)
        {
            return new AutoModelBuilder().AddTypeSource(source);
        }

        public static AutoModelBuilder Source(ITypeSource source, IEntityAutoConfiguration configuration)
        {
            return new AutoModelBuilder(configuration).AddTypeSource(source);
        }

        public static AutoModelBuilder Assemblies(params Assembly[] assemblies)
        {
            return Source(new CombinedAssemblyTypeSource(assemblies.Select(x => new AssemblyTypeSource(x))));
        }

        public static AutoModelBuilder Assemblies(IEntityAutoConfiguration configuration, params Assembly[] assemblies)
        {
            return Source(new CombinedAssemblyTypeSource(assemblies.Select(x => new AssemblyTypeSource(x))),
                configuration);
        }

        public static AutoModelBuilder Assemblies(IEntityAutoConfiguration configuration,
            IEnumerable<Assembly> assemblies)
        {
            return Source(new CombinedAssemblyTypeSource(assemblies.Select(x => new AssemblyTypeSource(x))),
                configuration);
        }

        public static AutoModelBuilder Assembly(Assembly assembly)
        {
            return Source(new AssemblyTypeSource(assembly));
        }

        public static AutoModelBuilder Assembly(Assembly assembly, IEntityAutoConfiguration configuration)
        {
            return Source(new AssemblyTypeSource(assembly), configuration);
        }

        public static AutoModelBuilder AssemblyOf<T>()
        {
            return Assembly(typeof(T).GetTypeInfo().Assembly);
        }

        public static AutoModelBuilder AssemblyOf<T>(IEntityAutoConfiguration configuration)
        {
            return Assembly(typeof (T).GetTypeInfo().Assembly, configuration);
        }
    }
}