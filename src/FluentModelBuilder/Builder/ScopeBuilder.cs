using System;

namespace FluentModelBuilder.Builder
{
    public class ScopeBuilder
    {
        private readonly AutoModelBuilder _autoModelBuilder;

        public ScopeBuilder(AutoModelBuilder autoModelBuilder)
        {
            if (autoModelBuilder == null)
                throw new ArgumentNullException(nameof(autoModelBuilder));
            _autoModelBuilder = autoModelBuilder;
        }

        /// <summary>
        ///     Instructs this AutoModelBuilder to only execute on PreModelCreating
        /// </summary>
        /// <returns></returns>
        public AutoModelBuilder PreModelCreating()
        {
            _autoModelBuilder.UseScope(BuilderScope.PreModelCreating);
            return _autoModelBuilder;
        }

        /// <summary>
        ///     Instructs this AutoModelBuilder to only execute on PostModelCreating
        /// </summary>
        /// <returns></returns>
        public AutoModelBuilder PostModelCreating()
        {
            _autoModelBuilder.UseScope(BuilderScope.PostModelCreating);
            return _autoModelBuilder;
        }

        /// <summary>
        ///     Instructs this AutoModelBuilder to decide execution scope through configuration, which defaults to
        ///     PostModelCreating
        /// </summary>
        /// <returns></returns>
        public AutoModelBuilder Default()
        {
            _autoModelBuilder.UseScope(null);
            return _autoModelBuilder;
        }
    }
}