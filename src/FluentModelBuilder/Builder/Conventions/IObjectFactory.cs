using System;
using System.Linq;
using System.Reflection;
using FluentModelBuilder.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace FluentModelBuilder.Builder.Conventions
{
    public interface IObjectFactory<out T>
    {
        T Create(IServiceProvider serviceProvider);
    }

}