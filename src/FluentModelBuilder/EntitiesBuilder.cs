using System;
using System.Collections.Generic;
using System.Linq;
using FluentModelBuilder.Contributors;
using Microsoft.Data.Entity;

namespace FluentModelBuilder
{
    public class EntitiesBuilder
    {
        public IList<IEntityContributor> Contributors { get; set; } = new List<IEntityContributor>();

        public EntitiesBuilder WithContributor<T>(Action<T> contributorAction = null) where T : IEntityContributor, new()
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

        public EntitiesBuilder AddContributor(IEntityContributor contributor)
        {
            if(!Contributors.Contains(contributor))
                Contributors.Add(contributor);
            return this;
        }

        public void Apply(ModelBuilder modelBuilder)
        {
            foreach (var source in Contributors)
                source.Contribute(modelBuilder);
        }
    }
}