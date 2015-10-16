using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Microsoft.Framework.DependencyInjection;

namespace FluentModelBuilder
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
