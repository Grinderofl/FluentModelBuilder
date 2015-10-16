using System.Reflection;

namespace FluentModelBuilder.Contributors.Internal.Criteria
{
    public class NonAbstractCriterion : ITypeInfoCriterion
    {
        public bool IsSatisfiedBy(TypeInfo typeInfo) => !typeInfo.IsAbstract;
    }
}