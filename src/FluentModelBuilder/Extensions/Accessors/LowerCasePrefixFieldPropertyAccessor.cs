namespace FluentModelBuilder.Extensions.Accessors
{
    public class LowerCasePrefixFieldPropertyAccessor : LowerCaseFieldPropertyAccessor
    {
        protected override string CreateFieldName(string propertyName)
        {
            return $"_{base.CreateFieldName(propertyName)}";
        }
    }
}