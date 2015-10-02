using ConventionModelBuilder.Options;

namespace ConventionModelBuilder.Sqlite.Extensions
{
    public static class ConventionModelBuilderOptionsExtensions
    {
        public static ConventionModelBuilderOptions UseSqlServer(this ConventionModelBuilderOptions options, bool useCoreConventions = true)
        {
            options.ConventionSetSource = new SqliteConventionSetSource(useCoreConventions);
            return options;
        }
    }
}
