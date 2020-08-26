namespace SomeProjectContainingClasses
{
    public class ThisIsAClassWithFourPublicConstructors
    {
        public ThisIsAClassWithFourPublicConstructors()
        {
        }

        public ThisIsAClassWithFourPublicConstructors(string summat)
        {
            if (null == summat) throw new System.ArgumentNullException(nameof(summat));
        }

        public ThisIsAClassWithFourPublicConstructors(string summat, string summatElse)
        {
            if (null == summat) throw new System.ArgumentNullException(nameof(summat));
            if (null == summatElse) throw new System.ArgumentNullException(nameof(summatElse));
        }
    }
}
