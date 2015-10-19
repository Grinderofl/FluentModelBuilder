using System.Collections.Generic;
using System.Reflection;

namespace FluentModelBuilder.Contributors.Core.Criteria
{
    public class BaseTypeCriterion : ITypeInfoCriterion
    {
        public IList<TypeInfo> Types { get; } = new List<TypeInfo>();

        public void AddType(TypeInfo typeInfo)
        {
            if(!Types.Contains(typeInfo))
                Types.Add(typeInfo);
        }
        public bool IsSatisfiedBy(TypeInfo typeInfo) => Types.Contains(typeInfo.BaseType.GetTypeInfo());
    }
}