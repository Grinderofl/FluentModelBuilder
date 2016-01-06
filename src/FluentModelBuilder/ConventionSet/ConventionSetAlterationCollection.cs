using FluentModelBuilder.Alterations;
using FluentModelBuilder.Core;

namespace FluentModelBuilder.ConventionSet
{
    public class ConventionSetAlterationCollection :
        AlterationCollectionBase<ConventionSetAlterationCollection, IConventionSetAlteration>
    {
        protected internal void Apply(Microsoft.Data.Entity.Metadata.Conventions.ConventionSet set)
        {
            foreach (var alteration in Alterations)
                alteration.Alter(set);
        }
    }
}