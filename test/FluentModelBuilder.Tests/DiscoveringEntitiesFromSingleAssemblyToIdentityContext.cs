using System.Linq;
using FluentModelBuilder.Core.Contributors.Extensions;
using FluentModelBuilder.Extensions;
using FluentModelBuilder.InMemory.Extensions;
using FluentModelBuilder.Tests.Core;
using FluentModelBuilder.Tests.Entities;
using FluentModelBuilder.TestTarget;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FluentModelBuilder.Tests
{
    public class DiscoveringEntitiesFromSingleAssemblyToIdentityContext : TestBase
    {

        public DiscoveringEntitiesFromSingleAssemblyToIdentityContext(ModelFixture fixture) : base(fixture)
        {
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFramework().AddDbContext<IdentityContext>(ConfigureOptions).AddInMemoryFluentModelBuilder();
            services.AddIdentity<TestUser, TestRole>().AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();
            services.AddScoped<DbContext>(x => x.GetService<IdentityContext>());
        }

        protected override void ConfigureOptions(DbContextOptionsBuilder options)
        {
            options.ConfigureModel()
                .DiscoverEntitiesFromSharedAssemblies(x => x.NotAbstract().BaseType<EntityBase>())
                .AddAssemblyContaining<EntityBase>()
                .WithInMemoryDatabase();
        }

        public class IdentityContext : IdentityDbContext<TestUser>
        {
        }

        public class TestUser : IdentityUser
        {
        }

        public class TestRole : IdentityRole
        {
        }

        [Fact]
        public void AddsEntity()
        {
            Assert.True(Model.GetEntityTypes().OrderBy(x => x.Name).Any(x => x.ClrType == typeof(EntityOne)));
            Assert.True(Model.GetEntityTypes().OrderBy(x => x.Name).Any(x => x.ClrType == typeof(EntityTwo)));
        }
    }
}