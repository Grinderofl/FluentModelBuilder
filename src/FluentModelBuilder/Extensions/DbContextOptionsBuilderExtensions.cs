using System;
using FluentModelBuilder.Builder;
using FluentModelBuilder.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace FluentModelBuilder.Extensions
{
    public static class DbContextOptionsBuilderExtensions
    {
        /// <summary>
        ///     Fluently configures AutoModelBuilder for Entity Framework for application
        /// </summary>
        /// <param name="optionsBuilder">DbContestOptionsBuilder</param>
        /// <param name="action">AutoModelBuilder</param>
        /// <returns>DbContextOptionsBuilder</returns>
        public static DbContextOptionsBuilder Configure(this DbContextOptionsBuilder optionsBuilder,
            Action<FluentModelBuilderConfiguration> action)
        {
            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(
                new FluentModelBuilderOptionsExtension());
            var builder = new FluentModelBuilderOptionsBuilder(optionsBuilder);
            builder.Configuration(action);
            return optionsBuilder;
        }

        /// <summary>
        ///     Fluently configures AutoModelBuilder for Entity Framework for application
        /// </summary>
        /// <param name="optionsBuilder">DbContestOptionsBuilder</param>
        /// <param name="builders">AutoModelBuilders</param>
        /// <returns>DbContextOptionsBuilder</returns>
        public static DbContextOptionsBuilder Configure(this DbContextOptionsBuilder optionsBuilder,
            params AutoModelBuilder[] builders)
        {
            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(
                new FluentModelBuilderOptionsExtension());
            var builder = new FluentModelBuilderOptionsBuilder(optionsBuilder);
            builder.Configuration(x =>
            {
                foreach (var mBuilder in builders)
                    x.Add(mBuilder);
            });
            return optionsBuilder;
        }
    }
}