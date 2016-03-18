using System;
using FluentModelBuilder.Builder;
using Microsoft.EntityFrameworkCore;

namespace FluentModelBuilder.Configuration
{
    public class ExpressionBasedEntityAutoConfiguration : DefaultEntityAutoConfiguration
    {
        private readonly AutoConfigurationExpressions _expressions;

        public ExpressionBasedEntityAutoConfiguration(AutoConfigurationExpressions expressions)
        {
            _expressions = expressions;
        }

        public override bool ShouldMap(Type type)
            => _expressions.ShouldMap?.Invoke(type) ?? base.ShouldMap(type);

        public override bool ShouldApplyToContext(DbContext context)
            => _expressions.ShouldApplyToContext?.Invoke(context) ?? base.ShouldApplyToContext(context);

        public override bool ShouldApplyToScope(BuilderScope scope)
            => _expressions.ShouldApplyToScope?.Invoke(scope) ?? base.ShouldApplyToScope(scope);
    }
}