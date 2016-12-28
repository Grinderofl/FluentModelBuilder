namespace FluentModelBuilder.Extensions.Accessors
{
    public class LowerCaseFieldPropertyAccessor : FieldPropertyAccessor
    {
        protected override string CreateFieldName(string propertyName)
        {
            return propertyName.ToLowerInvariant();
        }
    }
}