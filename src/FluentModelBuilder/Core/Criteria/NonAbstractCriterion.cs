using System.Reflection;

namespace FluentModelBuilder.Core.Criteria
{
    public class NonAbstractCriterion : ITypeInfoCriterion
    {
        public bool IsSatisfiedBy(TypeInfo typeInfo)
        {
            return !typeInfo.IsAbstract;
        }
    }
}