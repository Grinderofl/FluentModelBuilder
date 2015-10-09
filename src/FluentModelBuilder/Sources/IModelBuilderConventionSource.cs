using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentModelBuilder.Conventions;

namespace FluentModelBuilder.Sources
{
    public interface IModelBuilderConventionSource
    {
        IList<IModelBuilderConvention> ModelBuilderConventions { get; }
    }
}
