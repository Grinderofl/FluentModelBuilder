using System;
using System.Reflection;

namespace ConventionModelBuilder.Conventions.Criteria
{
    public class ExpressionCriteria : ITypeInfoCriteria
    {
        private readonly Func<TypeInfo, bool> _expression;

        public ExpressionCriteria(Func<TypeInfo, bool> expression)
        {
            _expression = expression;
        }

        public bool IsSatisfiedBy(TypeInfo typeInfo)
        {
            return _expression(typeInfo);
        }
    }
}