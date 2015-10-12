using System;
using Microsoft.Data.Entity.Metadata.Builders;

namespace FluentModelBuilder.v2
{
    public static class EntityCoreDescriptorExtensions
    {
        public static EntityCoreDescriptor Discover(this EntityCoreDescriptor descriptor, Action<DiscoveryOptions> optionsAction = null)
        {
            var discoveryDescriptor = new DiscoveryDescriptor();
            optionsAction?.Invoke(discoveryDescriptor.Options);
            descriptor.EntityDescriptors.Add(discoveryDescriptor);
            return descriptor;
        }

        public static EntityCoreDescriptor Add<T>(this EntityCoreDescriptor descriptor)
        {
            descriptor.EntityDescriptors.Add(new SingleEntityDescriptor(typeof(T)));
            return descriptor;
        }

        public static EntityCoreDescriptor Add<T>(this EntityCoreDescriptor descriptor,
            Action<EntityTypeBuilder<T>> builderAction) where T : class
        {
            descriptor.EntityDescriptors.Add(new EntityConfigurationDescriptor<T>(builderAction));
            return descriptor;
        }
    }
}