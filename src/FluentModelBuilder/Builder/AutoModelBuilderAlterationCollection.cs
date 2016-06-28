using System.Linq;
using FluentModelBuilder.Alterations;
using FluentModelBuilder.Core;

namespace FluentModelBuilder.Builder
{
    public class AutoModelBuilderAlterationCollection :
        AlterationCollectionBase<AutoModelBuilderAlterationCollection, IAutoModelBuilderAlteration>
    {
        protected internal void Apply(AutoModelBuilder builder)
        {
            foreach (var alteration in Alterations.ToList())
                alteration.Alter(builder);
        }
    }
}