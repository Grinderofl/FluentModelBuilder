using System;
using System.Collections.Generic;
using Microsoft.Data.Entity;

namespace FluentModelBuilder
{
    public class ListEntityContributor : IEntityContributor
    {
        private readonly IList<Type> _types = new List<Type>();

        public void Add(Type type)
        {
            if(!_types.Contains(type))
                _types.Add(type);
        }

        public void Contribute(ModelBuilder modelBuilder)
        {
            foreach (var type in _types)
                modelBuilder.Entity(type);
        }
    }
}