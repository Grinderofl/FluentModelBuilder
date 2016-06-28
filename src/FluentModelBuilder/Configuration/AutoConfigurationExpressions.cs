using System;
using FluentModelBuilder.Builder;
using Microsoft.EntityFrameworkCore;

namespace FluentModelBuilder.Configuration
{
    public class AutoConfigurationExpressions
    {
        public Func<DbContext, bool> ShouldApplyToContext;

        public Func<BuilderScope, bool> ShouldApplyToScope;
        public Func<Type, bool> ShouldMap;
    }
}