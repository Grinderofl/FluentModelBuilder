using FluentModelBuilder.Builder;
using FluentModelBuilder.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FluentModelBuilder.Conventions
{
    //public class FluentModelBuilderConvention : IModelConvention
    //{
    //    private readonly FluentModelBuilderConfiguration _configuration;
    //    private readonly BuilderScope _scope;

    //    public FluentModelBuilderConvention(FluentModelBuilderConfiguration configuration, BuilderScope scope)
    //    {
    //        _configuration = configuration;
    //        _scope = scope;
    //    }

    //    public InternalModelBuilder Apply(InternalModelBuilder modelBuilder)
    //    {
    //        _configuration.Apply(modelBuilder, _scope);
    //        return modelBuilder;
    //    }

    //}
}