using FluentModelBuilder.Builder;

namespace FluentModelBuilder.Alterations
{
    public interface IAutoModelBuilderAlteration
    {
        void Alter(AutoModelBuilder builder);
    }
}