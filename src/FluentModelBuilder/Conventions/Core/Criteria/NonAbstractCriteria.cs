using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FluentModelBuilder.Conventions.Core.Criteria
{
    public class NonAbstractCriteria : ITypeInfoCriteria
    {
        public bool IsSatisfiedBy(TypeInfo typeInfo)
        {
            return !typeInfo.IsAbstract;
        }
    }
}
