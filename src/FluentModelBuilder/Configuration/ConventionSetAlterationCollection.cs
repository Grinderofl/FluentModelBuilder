using FluentModelBuilder.Alterations;
using FluentModelBuilder.Core;
using Microsoft.Data.Entity.Metadata.Conventions;

namespace FluentModelBuilder.Configuration
{
    public class ConventionSetAlterationCollection :
        AlterationCollectionBase<ConventionSetAlterationCollection, IConventionSetAlteration>
    {
        protected internal void Apply(ConventionSet set)
        {
            foreach (var alteration in Alterations)
                alteration.Alter(set);
        }
    }
}