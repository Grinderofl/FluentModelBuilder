using System;
using Microsoft.EntityFrameworkCore;

namespace FluentModelBuilder.Alterations
{
    public interface IModelBuilderOverride
    {
        void Override(ModelBuilder modelBuilder);
    }
}