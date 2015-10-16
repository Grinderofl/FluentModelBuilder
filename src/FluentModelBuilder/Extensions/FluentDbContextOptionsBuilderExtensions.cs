namespace FluentModelBuilder
{
    public static class FluentDbContextOptionsBuilderExtensions
    {
        /// <summary>
        /// Adds an extension to Fluent Builder
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static FluentDbContextOptionsBuilder WithExtension<T>(this FluentDbContextOptionsBuilder builder)
            where T : IBuilderExtension, new()
        {
            return builder.WithExtension(new T());
        }
    }
}