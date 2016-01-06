using System;

namespace FluentModelBuilder.Builder
{
    public interface IEntityAutoConfiguration
    {
        bool ShouldMap(Type type);    
    }
}