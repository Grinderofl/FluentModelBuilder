namespace FluentModelBuilder.Extensions.Accessors
{
    public class CamelCasePrefixFieldPropertyAccessor : CamelCaseFieldPropertyAccessor
    {
        protected override string CreateFieldName(string propertyName)
        {
            return $"_{base.CreateFieldName(propertyName)}";
        }
    }
}