using System;

namespace FluentModelBuilder
{
    public static class EntitiesBuilderExtensions
    {
        public static EntitiesBuilder Add<T>(this EntitiesBuilder builder)
        {
            return builder.Add(typeof (T));
        }

        public static EntitiesBuilder AddContributor<T>(this EntitiesBuilder builder) where T : IEntityContributor, new()
        {
            builder.Contributors.Add(new T());
            return builder;
        }

        public static EntitiesBuilder AddContributor(this EntitiesBuilder builder, IEntityContributor contributor)
        {
            builder.Contributors.Add(contributor);
            return builder;
        }

        public static EntitiesBuilder Add(this EntitiesBuilder builder, Type type)
        {
            builder.AddContributor(new SingleEntityContributor(type));
            return builder;
        }
    }
}