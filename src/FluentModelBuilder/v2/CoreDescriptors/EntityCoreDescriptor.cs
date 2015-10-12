using System.Collections.Generic;
using Microsoft.Framework.DependencyInjection;

namespace FluentModelBuilder.v2
{
    public class EntityCoreDescriptor : AbstractCoreDescriptor
    {
        public IList<IDescriptor> EntityDescriptors { get; } = new List<IDescriptor>();

        public override void ApplyServices(IServiceCollection services)
        {
            foreach(var descriptor in EntityDescriptors)
                descriptor.ApplyServices(services);
            services.AddSingleton<IModelBuilderConvention, EntityAddingConvention>();
        }

        public EntityCoreDescriptor(IList<ICoreDescriptor> coreDescriptors) : base(coreDescriptors)
        {
        }
    }
}