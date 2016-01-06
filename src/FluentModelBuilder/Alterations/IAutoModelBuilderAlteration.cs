namespace FluentModelBuilder.Alterations
{
    public interface IAutoModelBuilderAlteration
    {
        void Alter(AutoModelBuilder.AutoModelBuilder builder);
    }
}