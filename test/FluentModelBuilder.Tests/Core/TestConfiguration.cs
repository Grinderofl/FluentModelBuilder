using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentModelBuilder.Configuration;
using FluentModelBuilder.TestTarget;

namespace FluentModelBuilder.Tests.Core
{
    public class TestConfiguration : DefaultEntityAutoConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            return type.GetTypeInfo().IsSubclassOf(typeof (EntityBase));
        }
    }
}
