using FluentModelBuilder.Options;

namespace FluentModelBuilder.Sqlite.Extensions
{
    public static class FluentModelBuilderOptionsExtensions
    {
        public static FluentModelBuilderOptions UseSqlServer(this FluentModelBuilderOptions options, bool useCoreConventions = true)
        {
            options.ConventionSetSource = new SqliteConventionSetSource(useCoreConventions);
            return options;
        }
    }
}
