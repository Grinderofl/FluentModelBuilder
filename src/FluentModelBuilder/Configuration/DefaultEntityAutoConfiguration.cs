using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using FluentModelBuilder.Alterations;
using FluentModelBuilder.Builder;
using FluentModelBuilder.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FluentModelBuilder.Configuration
{
    public class DefaultEntityAutoConfiguration : IEntityAutoConfiguration
    {
        public virtual bool ShouldMap(Type type)
        {
            return !type.ClosesInterface(typeof (IEntityTypeOverride<>)) &&
                   !type.GetTypeInfo().IsNestedPrivate &&
                   !type.GetTypeInfo().IsDefined(typeof (CompilerGeneratedAttribute), false) &&
                   type.GetTypeInfo().IsClass;
        }

        public bool ShouldApplyToContext(DbContext context)
        {
            return true;
        }

        public bool ShouldApplyToScope(BuilderScope scope)
        {
            return true;
        }
    }
}