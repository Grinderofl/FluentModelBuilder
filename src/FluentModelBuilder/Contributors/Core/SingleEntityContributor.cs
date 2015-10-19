using System;
using Microsoft.Data.Entity;

namespace FluentModelBuilder.Contributors.Core
{
    public class SingleEntityContributor : IEntityContributor
    {
        private readonly Type _type;

        public SingleEntityContributor(Type type)
        {
            _type = type;
        }

        public void Contribute(ModelBuilder builder)
        {
            builder.Entity(_type);
        }
    }
}
