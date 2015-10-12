using System;
using System.Reflection;

namespace FluentModelBuilder.v2
{
    public static class AssemblyCoreDescriptorOptionsExtensions
    {
        public static AssemblyCoreDescriptorOptions Single(this AssemblyCoreDescriptorOptions options, Assembly assembly)
        {
            options.Assemblies.AddIfNotExists(assembly);
            return options;
        }

        public static AssemblyCoreDescriptorOptions Containing(this AssemblyCoreDescriptorOptions options, Type type)
        {
            return options.Single(type.GetTypeInfo().Assembly);
        }

        public static AssemblyCoreDescriptorOptions Containing<T>(this AssemblyCoreDescriptorOptions options)
        {
            return options.Containing(typeof(T));
        }
    }
}