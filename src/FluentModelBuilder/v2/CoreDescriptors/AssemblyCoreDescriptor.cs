using System.Collections.Generic;
using Microsoft.Framework.DependencyInjection;

namespace FluentModelBuilder.v2
{
    public class AssemblyCoreDescriptor : AbstractCoreDescriptor
    {
        public AssemblyCoreDescriptorOptions Options { get; } = new AssemblyCoreDescriptorOptions();
        public override void ApplyServices(IServiceCollection services)
        {
            services.AddInstance<ISharedAssemblySource>(new SharedAssemblySource(Options.Assemblies));
        }

        public AssemblyCoreDescriptor(IList<ICoreDescriptor> coreDescriptors) : base(coreDescriptors)
        {
        }
    }
}