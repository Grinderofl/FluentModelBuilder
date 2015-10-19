using System;
using System.Collections.Generic;
using FluentModelBuilder.Contributors;
using System.Linq;
using Microsoft.Data.Entity;

namespace FluentModelBuilder
{
    public class OverridesBuilder
    {
        public IList<IOverrideContributor> Contributors { get; set; } = new List<IOverrideContributor>();

        public OverridesBuilder WithContributor<T>(Action<T> contributorAction = null) where T : IOverrideContributor, new()
        {
            var contributor = (T) (Contributors.FirstOrDefault(x => x is T) ?? ((Func<T>) (() =>
            {
                var t = new T();
                Contributors.Add(t);
                return t;
            }))());

            contributorAction?.Invoke(contributor);
            return this;
        }

        public OverridesBuilder AddContributor(IOverrideContributor contributor)
        {
            if (!Contributors.Contains(contributor))
                Contributors.Add(contributor);
            return this;
        }

        public void Apply(ModelBuilder modelBuilder)
        {
            foreach(var source in Contributors)
                source.Contribute(modelBuilder);
        }
    }
}