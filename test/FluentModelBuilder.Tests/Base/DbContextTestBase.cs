using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentModelBuilder.Builder;
using FluentModelBuilder.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace FluentModelBuilder.Tests.Base
{
    public abstract class DbContextTestBase
    {
        protected virtual IModel CreateModel(AutoModelBuilder @from)
        {
            var services = new ServiceCollection();
            services.AddEntityFrameworkInMemoryDatabase().ConfigureEntityFramework(from);
            var provider = services.BuildServiceProvider();
            return new TestContext(provider).Model;
        }

        protected class TestContext : DbContext
        {
            private readonly IServiceProvider _provider;

            public TestContext()
            {
                
            }
            
            public TestContext(IServiceProvider provider)
            {
                _provider = provider;
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                base.OnConfiguring(optionsBuilder);
                optionsBuilder.UseInternalServiceProvider(_provider).UseInMemoryDatabase();
            }
        }
    }
}
