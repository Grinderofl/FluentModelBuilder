using System;
using System.Collections.Generic;
using FluentModelBuilder.Builder;
using Microsoft.Data.Entity.Metadata.Internal;

namespace FluentModelBuilder.Configuration
{
    public class FluentModelBuilderConfiguration
    {
        private readonly IList<AutoModelBuilder> _builders = new List<AutoModelBuilder>();

        public FluentModelBuilderConfiguration Add(AutoModelBuilder builder)
        {
            _builders.Add(builder);
            return this;
        }

        public FluentModelBuilderConfiguration Add(Func<AutoModelBuilder> builder)
        {
            _builders.Add(builder());
            return this;
        }

        public ConventionSetAlterationCollection Alterations = new ConventionSetAlterationCollection();

        internal void Apply(InternalModelBuilder builder)
        {
            foreach(var b in _builders)
                b.Apply(builder);
        }

        internal void Apply(Microsoft.Data.Entity.Metadata.Conventions.ConventionSet conventionSet)
        {
            foreach(var alteration in Alterations)
                alteration.Alter(conventionSet);
        }
    }
}