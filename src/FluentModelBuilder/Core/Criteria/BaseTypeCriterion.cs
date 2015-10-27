using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FluentModelBuilder.Core.Criteria
{
    public class BaseTypeCriterion : ITypeInfoCriterion
    {
        public IList<TypeInfo> Types { get; } = new List<TypeInfo>();

        public void AddType(TypeInfo typeInfo)
        {
            if(!Types.Contains(typeInfo))
                Types.Add(typeInfo);
        }

        public bool IsSatisfiedBy(TypeInfo typeInfo)
        {
            return Types.Any(type => type.IsAssignableFrom(typeInfo));
        }
    }
}