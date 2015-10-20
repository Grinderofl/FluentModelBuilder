﻿using Microsoft.Data.Entity.Metadata.Builders;

namespace FluentModelBuilder
{
    /// <summary>
    /// Allows overriding the configuration for single entity type while configuring ModelBuilder.
    /// <remarks>Currently in place due to missing EntityTypeOverride.</remarks>
    /// </summary>
    /// <typeparam name="T">Type of entity to configure</typeparam>
    public interface IEntityTypeOverride<T> where T : class
    {
        void Configure(EntityTypeBuilder<T> mapping);
    }
}