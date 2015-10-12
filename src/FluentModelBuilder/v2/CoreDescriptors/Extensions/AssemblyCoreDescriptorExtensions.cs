using System;
using System.Reflection;

namespace FluentModelBuilder.v2
{
    public static class AssemblyCoreDescriptorExtensions
    {
        public static AssemblyCoreDescriptor AddAssemblyContaining<T>(this AssemblyCoreDescriptor descriptor)
        {
            var assembly = typeof (T).GetTypeInfo().Assembly;
            return descriptor.AddAssembly(assembly);
        }

        public static AssemblyCoreDescriptor Add(this AssemblyCoreDescriptor descriptor, Assembly assembly)
        {
            descriptor.Options.Assemblies.AddIfNotExists(assembly);
            return descriptor;
        }

        public static AssemblyCoreDescriptor AddAssembly(this AssemblyCoreDescriptor descriptor, Assembly assembly)
        {
            descriptor.Options.Assemblies.AddIfNotExists(assembly);
            return descriptor;
        }

        public static AssemblyCoreDescriptor Add(this AssemblyCoreDescriptor descriptor, Action<AssemblyCoreDescriptorOptions> optionsAction = null)
        {
            optionsAction?.Invoke(descriptor.Options);
            return descriptor;
        }
    }
}