using System;
using System.Collections.Generic;
using FluentModelBuilder.Builder;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace FluentModelBuilder.Configuration
{
    public class FluentModelBuilderConfiguration
    {
        private readonly IList<AutoModelBuilder> _builders = new List<AutoModelBuilder>();

        public ConventionSetAlterationCollection Alterations = new ConventionSetAlterationCollection();

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

        internal void Apply(BuilderContext parameters)
        {
            foreach (var b in _builders)
                b.Apply(parameters);
        }

        internal void Apply(ConventionSet conventionSet)
        {
            foreach (var alteration in Alterations)
                alteration.Alter(conventionSet);
        }
    }
}