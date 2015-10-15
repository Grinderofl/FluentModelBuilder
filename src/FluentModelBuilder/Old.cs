using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Framework.DependencyInjection;

namespace FluentModelBuilder
{
    public interface IModelBuilderContributor
    {
        void Contribute(ModelBuilder modelBuilder);
    }

    public interface IFluentBuilderContributor : IModelBuilderContributor
    { }

    public interface IBuilderExtension
    {
        void ApplyServices(DbContextOptionsBuilder builder);
    }


    public class EntitiesBuilder
    {
        public IList<IEntityContributor> Contributors { get; set; } = new List<IEntityContributor>();
        
        public void Apply(ModelBuilder modelBuilder)
        {
            foreach (var source in Contributors)
                source.Contribute(modelBuilder);
        }
    }

    public interface IModelSourceProvider
    {
        void ApplyServices(EntityFrameworkServicesBuilder builder);
    }
    
    public interface IServiceApplier
    {
        void ApplyServices(IServiceCollection services);
    }

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
