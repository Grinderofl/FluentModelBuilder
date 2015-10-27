using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentModelBuilder.TestTarget
{
    public abstract class EntityBase
    {
        public int Id { get; set; }
    }

    public abstract class EntityBaseWithoutId
    {
        public string Name { get; set; }
    }

    public abstract class EntityBaseWithGenericId<T> : EntityBaseWithoutId
    {
        public T Id { get; set; }
    }

    public class EntityWithIntId : EntityBaseWithGenericId<int>
    {
        public string Property { get; set; }
    }
}
