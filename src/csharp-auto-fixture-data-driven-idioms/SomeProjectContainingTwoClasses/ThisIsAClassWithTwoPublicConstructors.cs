namespace SomeProjectContainingTwoClasses
{
    public class ThisIsAClassWithTwoPublicConstructors
    {
        public ThisIsAClassWithTwoPublicConstructors()
        {
        }

        public ThisIsAClassWithTwoPublicConstructors(string something)
        {
            if (null == something) throw new System.ArgumentNullException(nameof(something));
        }
    }
}
