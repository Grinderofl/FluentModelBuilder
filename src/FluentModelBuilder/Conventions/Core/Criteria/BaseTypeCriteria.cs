using System;
using System.Reflection;

namespace FluentModelBuilder.Conventions.Core.Criteria
{
    public class BaseTypeCriteria : ITypeInfoCriteria
    {
        internal readonly Type Type;

        public BaseTypeCriteria(Type type)
        {
            Type = type;
        }

        public bool IsSatisfiedBy(TypeInfo typeInfo)
        {
            return Type.IsAssignableFrom(typeInfo.AsType());
        }
    }
}