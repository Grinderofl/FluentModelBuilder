using FluentModelBuilder.Alterations;
using FluentModelBuilder.Core;

namespace FluentModelBuilder.AutoModelBuilder
{
    public class AutoModelBuilderAlterationCollection : AlterationCollectionBase<AutoModelBuilderAlterationCollection, IAutoModelBuilderAlteration>
    {
        protected internal void Apply(AutoModelBuilder builder)
        {
            foreach (var alteration in Alterations)
                alteration.Alter(builder);
        }
    }
}