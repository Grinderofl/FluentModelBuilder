using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Framework.DependencyInjection;

namespace FluentModelBuilder
{
    public interface IFluentBuilderApplier
    {
        void Apply(ModelBuilder modelBuilder, DbContext context);
    }

    public interface IModelSourceBuilder
    {
        void ApplyServices(IServiceCollection services);
        void ApplyServices(EntityFrameworkServicesBuilder builder);
    }

    public static class EntitiesBuilderExtensions
    {
        public static EntitiesBuilder Add<T>(this EntitiesBuilder builder)
        {
            return builder.Add(typeof (T));
        }

        public static EntitiesBuilder Add(this EntitiesBuilder builder, Type type)
        {
            builder.Sources.Add(new SingleEntityApplier(type));
            return builder;
        }
    }

    public class EntitiesBuilder
    {
        public IList<IEntityApplier> Sources { get; set; } = new List<IEntityApplier>();
        
        public void Apply(ModelBuilder modelBuilder)
        {
            foreach (var source in Sources)
                source.Apply(modelBuilder);
        }
    }

    public interface IProvider
    {
        void ApplyServices(EntityFrameworkServicesBuilder builder);
    }
    
    public interface IServiceApplier
    {
        void ApplyServices(IServiceCollection services);
    }


    public class SingleEntityApplier : IEntityApplier
    {
        private readonly Type _type;

        public SingleEntityApplier(Type type)
        {
            _type = type;
        }

        public void Apply(ModelBuilder builder)
        {
            builder.Entity(_type);
        }
    }
    
    public interface IEntityApplier
    {
        void Apply(ModelBuilder builder);
    }
}
