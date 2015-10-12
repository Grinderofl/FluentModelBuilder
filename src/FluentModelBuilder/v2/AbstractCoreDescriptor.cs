using System.Collections.Generic;

namespace FluentModelBuilder.v2
{
    public abstract class AbstractCoreDescriptor : CoreDescriptorBase
    {
        //public virtual EntityCoreDescriptor Entities => WithCoreDescriptor<EntityCoreDescriptor>();
        public virtual AssemblyCoreDescriptor Assemblies => WithCoreDescriptor<AssemblyCoreDescriptor>();
        public virtual OverrideCoreDescriptor Overrides => WithCoreDescriptor<OverrideCoreDescriptor>();

        protected AbstractCoreDescriptor(IList<ICoreDescriptor> coreDescriptors) : base(coreDescriptors)
        {
        }
    }
}