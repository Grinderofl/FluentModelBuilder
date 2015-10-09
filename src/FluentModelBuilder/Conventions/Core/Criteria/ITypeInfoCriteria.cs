using System.Reflection;

namespace FluentModelBuilder.Conventions.Core.Criteria
{
    /// <summary>
    /// Specifies a criteria to find corresponding entities with
    /// </summary>
    public interface ITypeInfoCriteria
    {
        bool IsSatisfiedBy(TypeInfo typeInfo);
    }
}