using Microsoft.Data.Entity;

namespace FluentModelBuilder.v2
{
    public static class DbContextOptionsExtensions
    {
        public static FluentModelBuilder BuildModel(this DbContextOptionsBuilder options)
        {
            var extension = options.Options.FindExtension<FluentModelBuilderExtension>();
            if (extension == null)
            {
                extension = new FluentModelBuilderExtension();
                options.Options.WithExtension(extension);
            }
            return extension.Builder;
        }
    }
}