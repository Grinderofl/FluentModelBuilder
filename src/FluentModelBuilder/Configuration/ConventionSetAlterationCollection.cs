using System.Reflection;
using FluentModelBuilder.Alterations;
using FluentModelBuilder.Core;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

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

        ///// <summary>
        ///// Adds individual conventionset alterations from specified assembly
        ///// </summary>
        ///// <remarks>
        ///// You would use this to alter a single ConventionSet property
        ///// </remarks>
        ///// <param name="assembly">Assembly to scan</param>
        //public void AddAlterationsFromAssembly(Assembly assembly)
        //{
        //    Alterations.Add(new ConventionAlteration(assembly));
        //}
    }
}