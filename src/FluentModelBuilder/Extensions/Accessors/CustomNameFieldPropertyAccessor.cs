namespace FluentModelBuilder.Extensions.Accessors
{
    public class CustomNameFieldPropertyAccessor : FieldPropertyAccessor
    {
        private readonly string _name;

        public CustomNameFieldPropertyAccessor(string name)
        {
            _name = name;
        }

        protected override string CreateFieldName(string propertyName)
        {
            return _name;
        }
    }
}