using System.Collections.Generic;
using Microsoft.Framework.DependencyInjection;

namespace FluentModelBuilder.v2
{
    public class OverrideCoreDescriptor : AbstractCoreDescriptor
    {
        public IList<IDescriptor> Descriptors { get; } = new List<IDescriptor>();

        public override void ApplyServices(IServiceCollection services)
        {
            
        }

        public OverrideCoreDescriptor(IList<ICoreDescriptor> coreDescriptors) : base(coreDescriptors)
        {
        }
    }
}