using System.Collections.Generic;
using Microsoft.Data.Entity;

namespace FluentModelBuilder
{
    public class EntitiesBuilder
    {
        public IList<IEntityContributor> Contributors { get; set; } = new List<IEntityContributor>();
        
        public void Apply(ModelBuilder modelBuilder)
        {
            foreach (var source in Contributors)
                source.Contribute(modelBuilder);
        }
    }
}