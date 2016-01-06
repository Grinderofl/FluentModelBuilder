using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.ChangeTracking.Internal;
using Microsoft.Data.Entity.Metadata.Internal;

namespace FluentModelBuilder
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

        internal void Apply(InternalModelBuilder builder)
        {
            foreach(var b in _builders)
                b.Apply(builder);
        }
    }
}