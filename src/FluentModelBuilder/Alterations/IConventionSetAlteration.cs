namespace FluentModelBuilder.Alterations
{
    public interface IConventionSetAlteration
    {
        void Alter(Microsoft.Data.Entity.Metadata.Conventions.ConventionSet conventions);
    }
}