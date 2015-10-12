using System;
using Microsoft.Framework.DependencyInjection;

namespace FluentModelBuilder.v2
{
    public class SingleEntityDescriptor : IDescriptor
    {
        private readonly Type _type;

        public SingleEntityDescriptor(Type type)
        {
            _type = type;
        }

        public void ApplyServices(IServiceCollection services)
        {
            services.AddInstance(typeof (ITypeSource), new SingleTypeSource(_type));
        }
    }
}