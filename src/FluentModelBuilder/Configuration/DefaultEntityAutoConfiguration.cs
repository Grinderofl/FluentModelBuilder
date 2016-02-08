using System;
using System.Reflection;
using FluentModelBuilder.Builder;
using Microsoft.EntityFrameworkCore;

namespace FluentModelBuilder.Configuration
{
    public class DefaultEntityAutoConfiguration : IEntityAutoConfiguration
    {
        public virtual bool ShouldMap(Type type)
        {
            return type.GetTypeInfo().IsClass;
        }

        public bool ShouldApplyToContext(DbContext context)
        {
            return true;
        }

        public bool ShouldApplyToScope(BuilderScope scope)
        {
            return scope == BuilderScope.PostModelCreating;
        }
    }



}