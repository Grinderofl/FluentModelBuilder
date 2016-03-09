using System;
using System.Reflection;
using FluentModelBuilder.Configuration;

namespace ModelBuilderSample
{
    public class ProgramConfiguration : DefaultEntityAutoConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            return base.ShouldMap(type) && type.GetTypeInfo().IsSubclassOf(typeof(Entity));
        }
    }
}