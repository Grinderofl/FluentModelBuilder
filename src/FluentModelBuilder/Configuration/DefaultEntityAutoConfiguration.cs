using System;
using System.Reflection;
using FluentModelBuilder.AutoModelBuilder;

namespace FluentModelBuilder.Configuration
{
    public class DefaultEntityAutoConfiguration : IEntityAutoConfiguration
    {
        public virtual bool ShouldMap(Type type)
        {
            return type.GetTypeInfo().IsClass;
        }
    }



}