using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentModelBuilder.Builder
{
    public class PropertyBuilder
    {
        private readonly AutoModelBuilder _autoModelBuilder;
        public PropertyBuilder(AutoModelBuilder autoModelBuilder)
        {
            _autoModelBuilder = autoModelBuilder;
        }

        public AutoModelBuilder PreModelCreating()
        {
            _autoModelBuilder.UseScope(BuilderScope.PreModelCreating);
            return _autoModelBuilder;
        }

        public AutoModelBuilder PostModelCreating()
        {
            _autoModelBuilder.UseScope(BuilderScope.PostModelCreating);
            return _autoModelBuilder;
        }
    }
}
