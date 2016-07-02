using FluentModelBuilder.Relational.Generators;

namespace FluentModelBuilder.Relational.Conventions
{
    public class PluralizingTableNameGeneratingConvention : TableNameGeneratingConvention
    {
        public PluralizingTableNameGeneratingConvention(bool shouldPluralize = true)
            : base(new PluralizingTableNameGenerator(shouldPluralize))
        {
        }
    }
}
