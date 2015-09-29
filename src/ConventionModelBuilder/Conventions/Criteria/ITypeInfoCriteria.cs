using System.Reflection;

namespace ConventionModelBuilder.Conventions.Criteria
{
    public interface ITypeInfoCriteria
    {
        bool IsSatisfiedBy(TypeInfo typeInfo);
    }
}