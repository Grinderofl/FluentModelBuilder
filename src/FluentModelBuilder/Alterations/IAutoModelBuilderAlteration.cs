using FluentModelBuilder.Builder;

namespace FluentModelBuilder.Alterations
{
    /// <summary>
    /// Provides a way to alter the AutoModelBuilder instance before model is built
    /// </summary>
    public interface IAutoModelBuilderAlteration
    {
        /// <summary>
        /// Alter the AutoModelBuilder
        /// </summary>
        /// <param name="builder">Instance of AutoModelBuilder to alter</param>
        void Alter(AutoModelBuilder builder);
    }
}