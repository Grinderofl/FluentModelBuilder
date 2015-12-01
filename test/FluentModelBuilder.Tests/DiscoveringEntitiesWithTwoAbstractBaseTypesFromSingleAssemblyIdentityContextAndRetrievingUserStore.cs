using System.Linq;
using System.Reflection;
using FluentModelBuilder.Core.Contributors.Extensions;
using FluentModelBuilder.Extensions;
using FluentModelBuilder.InMemory.Extensions;
using FluentModelBuilder.Tests.Core;
using FluentModelBuilder.TestTarget;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Internal;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FluentModelBuilder.Tests
{
    public class DiscoveringEntitiesWithTwoAbstractBaseTypesFromSingleAssemblyIdentityContextAndRetrievingUserStore : IClassFixture<ModelFixture>
    {
        private IModel Model;

        public DiscoveringEntitiesWithTwoAbstractBaseTypesFromSingleAssemblyIdentityContextAndRetrievingUserStore(ModelFixture fixture)
        {
            ConfigureServices(fixture.Services);
            var manager = fixture.Services.BuildServiceProvider().GetService<UserManager<TestUser>>();

            var storeField = manager.GetType().GetProperty("Store", BindingFlags.NonPublic | BindingFlags.Instance);
            var userStore = storeField.GetValue(manager);
            var cast = (UserStore<TestUser, IdentityRole, IdentityContext>)userStore;
            Model = cast.Context.Model;
        }

        protected void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFramework().AddDbContext<IdentityContext>(ConfigureOptions).AddInMemoryFluentModelBuilder();
            services.AddIdentity<TestUser, IdentityRole>().AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();
            services.AddScoped<DbContext>(x => x.GetService<IdentityContext>());
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        protected void ConfigureOptions(DbContextOptionsBuilder options)
        {
            options.ConfigureModel()
                .DiscoverEntitiesFromSharedAssemblies(x => x.BaseType<EntityBaseWithoutId>())
                .AddAssemblyContaining<EntityBaseWithoutId>()
                .WithInMemoryDatabase();
        }

        public class IdentityContext : IdentityDbContext<TestUser>
        {
        }

        public class TestUser : IdentityUser
        {
        }

        [Fact]
        public void DiscoversEntities()
        {
            Assert.True(Model.GetEntityTypes().Any(x => x.ClrType == typeof(EntityWithIntId)));
            //Assert.True(Model.GetEntityTypes().Any(x => x.ClrType == typeof(EntityTwo)));
        }

        [Fact]
        public void AddsAllEntities()
        {
            Assert.Equal(7, Model.GetEntityTypes().Count());
        }
    }
}