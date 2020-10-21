using System;

namespace IdiomaticTests.DataSource
{
    public class ConstructorFilterExcludeAttribute : Attribute
    {
        public readonly Type Type;
        public readonly string Because;

        public ConstructorFilterExcludeAttribute(Type type)
        {
            Type = type ?? throw new System.ArgumentNullException(nameof(type));
            Because = "just because";
        }

        public ConstructorFilterExcludeAttribute(Type type, string because)
        {
            Type = type ?? throw new System.ArgumentNullException(nameof(type));
            Because = because ?? throw new System.ArgumentNullException(nameof(because));
        }
    }
}
