using FluentModelBuilder.Relational.Generators;

namespace FluentModelBuilder.Relational.Conventions
{
    /// <summary>
    ///     Specifies whether table names should be pluralized
    /// </summary>
    public class PluralizingTableNameGeneratingConvention : TableNameGeneratingConvention
    {
        public PluralizingTableNameGeneratingConvention(bool shouldPluralize = true)
            : base(new PluralizingTableNameGenerator(shouldPluralize))
        {
        }
    }
}
