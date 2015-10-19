using System.Reflection;

namespace FluentModelBuilder.Core.Criteria
{
    public interface ITypeInfoCriterion
    {
        bool IsSatisfiedBy(TypeInfo typeInfo);
    }
}