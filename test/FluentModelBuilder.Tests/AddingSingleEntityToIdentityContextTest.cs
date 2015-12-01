using System.Linq;
using FluentModelBuilder.Extensions;
using FluentModelBuilder.InMemory.Extensions;
using FluentModelBuilder.Tests.Core;
using FluentModelBuilder.Tests.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FluentModelBuilder.Tests
{
    public class AddingSingleEntityToIdentityContextTest : TestBase
    {

        public AddingSingleEntityToIdentityContextTest(ModelFixture fixture) : base(fixture)
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
            options.ConfigureModel().Entities(x => x.Add<SingleEntity>()).WithInMemoryDatabase();
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
            Assert.True(Model.GetEntityTypes().OrderBy(x => x.Name).Any(x => x.ClrType == typeof(SingleEntity)));
        }
    }
}