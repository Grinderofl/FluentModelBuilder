using System;

namespace FluentModelBuilder
{
    public interface IEntityAutoConfiguration
    {
        bool ShouldMap(Type type);    
    }
}