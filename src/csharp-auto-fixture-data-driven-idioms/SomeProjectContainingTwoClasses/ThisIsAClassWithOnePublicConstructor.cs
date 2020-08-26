namespace SomeProjectContainingTwoClasses
{
    public class ThisIsAClassWithOnePublicConstructor
    {
        public ThisIsAClassWithOnePublicConstructor(string something)
        {
            if (null == something) throw new System.ArgumentNullException(nameof(something));
        }

        private ThisIsAClassWithOnePublicConstructor(string thisIsPrivate, string andSoIsThis)
        {
        }
    }
}
