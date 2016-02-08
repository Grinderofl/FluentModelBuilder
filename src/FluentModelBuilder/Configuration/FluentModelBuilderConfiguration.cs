using System;
using System.Collections.Generic;
using FluentModelBuilder.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

        internal void Apply(ModelBuilder builder, BuilderScope scope)
        {
            foreach(var b in _builders)
                b.Apply(builder, scope);
        }

        internal void Apply(ConventionSet conventionSet)
        {
            foreach(var alteration in Alterations)
                alteration.Alter(conventionSet);
        }
    }
}