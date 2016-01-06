using Microsoft.Data.Entity.Metadata.Conventions;

namespace FluentModelBuilder
{
    public interface IConventionSetAlteration
    {
        void Alter(ConventionSet conventions);
    }
}