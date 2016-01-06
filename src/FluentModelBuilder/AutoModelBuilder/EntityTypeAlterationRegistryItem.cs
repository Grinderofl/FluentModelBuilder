using System;

namespace FluentModelBuilder
{
    public class EntityTypeAlterationRegistryItem
    {
        public readonly Type Type;
        public readonly Type AlterationType;

        public EntityTypeAlterationRegistryItem(Type type, Type alterationType)
        {
            Type = type;
            AlterationType = alterationType;
        }

    }
}