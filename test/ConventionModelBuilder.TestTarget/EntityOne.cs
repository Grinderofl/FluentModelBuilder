namespace ConventionModelBuilder.TestTarget
{
    public class EntityOne : EntityBase
    {
        public string IgnoredInOverride { get; set; }
        public string NotIgnored { get; set; }
    }
}