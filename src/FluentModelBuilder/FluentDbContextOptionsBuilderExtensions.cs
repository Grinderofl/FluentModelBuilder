namespace FluentModelBuilder
{
    public static class FluentDbContextOptionsBuilderExtensions
    {
        public static FluentDbContextOptionsBuilder ModelSource<T>(this FluentDbContextOptionsBuilder builder)
            where T : IModelSourceBuilder, new()
        {
            return builder.ModelSource(new T());
        }
    }
}