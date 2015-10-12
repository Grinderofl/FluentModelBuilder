using System.Collections.Generic;
using System.Linq;
using Microsoft.Framework.DependencyInjection;

namespace FluentModelBuilder.v2
{
    public abstract class CoreDescriptorBase : ICoreDescriptor
    {
        protected IList<ICoreDescriptor> CoreDescriptors;

        public abstract void ApplyServices(IServiceCollection services);

        protected CoreDescriptorBase(IList<ICoreDescriptor> coreDescriptors)
        {
            CoreDescriptors = coreDescriptors;
        }

        public T WithCoreDescriptor<T>() where T : ICoreDescriptor
        {
            var descriptor = CoreDescriptors.SingleOrDefault(x => x is T);
            if (descriptor == null)
            {
                descriptor = ActivatorUtilities.CreateInstance<T>(null, CoreDescriptors);
                CoreDescriptors.Add(descriptor);
            }

            return (T)descriptor;
        }
    }
}