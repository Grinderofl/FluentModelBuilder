namespace FluentModelBuilder.v2
{
    public interface ICoreDescriptor : IDescriptor
    {
        T WithCoreDescriptor<T>() where T : ICoreDescriptor;
        //void ApplyServices(IServiceCollection services);
    }
}