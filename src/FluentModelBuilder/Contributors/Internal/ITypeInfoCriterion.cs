using System.Reflection;

namespace FluentModelBuilder
{
    public interface ITypeInfoCriterion
    {
        bool IsSatisfiedBy(TypeInfo typeInfo);
    }
}