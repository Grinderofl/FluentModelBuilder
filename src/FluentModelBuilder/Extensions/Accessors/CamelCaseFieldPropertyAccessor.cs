namespace FluentModelBuilder.Extensions.Accessors
{
    public class CamelCaseFieldPropertyAccessor : FieldPropertyAccessor
    {
        protected override string CreateFieldName(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                return "";

            var firstPart = propertyName.Substring(0, 1).ToLowerInvariant();
            var secondPart = propertyName.Substring(1);
            return $"{firstPart}{secondPart}";
        }
    }
}