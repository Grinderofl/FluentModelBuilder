using System.Reflection;

namespace FluentModelBuilder.Contributors.Core.Criteria
{
    public interface ITypeInfoCriterion
    {
        bool IsSatisfiedBy(TypeInfo typeInfo);
    }
}