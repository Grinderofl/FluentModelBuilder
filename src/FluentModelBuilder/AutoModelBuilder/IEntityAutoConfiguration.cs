using System;

namespace FluentModelBuilder.AutoModelBuilder
{
    public interface IEntityAutoConfiguration
    {
        bool ShouldMap(Type type);    
    }
}