using System;
using System.Reflection;

namespace FluentModelBuilder.Core.Criteria
{
    public class ExpressionCriterion: ITypeInfoCriterion
    {
        private readonly Func<TypeInfo, bool> _expression;

        public ExpressionCriterion(Func<TypeInfo, bool> expression)
        {
            _expression = expression;
        }

        public bool IsSatisfiedBy(TypeInfo typeInfo) => _expression(typeInfo);
    }
}