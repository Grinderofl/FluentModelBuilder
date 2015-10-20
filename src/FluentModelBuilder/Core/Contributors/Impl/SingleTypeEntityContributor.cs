using System;
using Microsoft.Data.Entity;

namespace FluentModelBuilder.Core.Contributors.Impl
{
    public class SingleTypeEntityContributor : IEntityContributor
    {
        private readonly Type _type;

        public SingleTypeEntityContributor(Type type)
        {
            _type = type;
        }

        public void Contribute(ModelBuilder builder)
        {
            builder.Entity(_type);
        }
    }
}
