using System.Reflection;

namespace FluentModelBuilder.Contributors.Internal.Criteria
{
    public interface ITypeInfoCriterion
    {
        bool IsSatisfiedBy(TypeInfo typeInfo);
    }
}