namespace FluentModelBuilder
{
    public static class FluentDbContextOptionsBuilderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static FluentDbContextOptionsBuilder ModelSource<T>(this FluentDbContextOptionsBuilder builder)
            where T : IBuilderExtension, new()
        {
            return builder.ModelSource(new T());
        }
    }
}