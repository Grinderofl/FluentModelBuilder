using Microsoft.Framework.DependencyInjection;

namespace FluentModelBuilder.v2
{
    public interface IDescriptor
    {
        void ApplyServices(IServiceCollection services);
    }
}