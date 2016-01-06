using System;
using System.Reflection;

namespace FluentModelBuilder
{
    public class DefaultEntityAutoConfiguration : IEntityAutoConfiguration
    {
        public virtual bool ShouldMap(Type type)
        {
            return type.GetTypeInfo().IsClass;
        }
    }



}