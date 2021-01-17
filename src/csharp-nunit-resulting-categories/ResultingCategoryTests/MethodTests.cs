using FluentAssertions;
using NUnit.Framework;
using System.Linq;

namespace ResultingCategoryTests
{
    public class WhenRetrievingCategoriesFromMethods
    {
        [Test]
        public void NoCategoriesAreRetrieved()
        {
            var provider = new ResultingCategoryProvider();

            var result = provider.GetCategories(TestContext.CurrentContext.Test);

            result.Should().HaveCount(0, because: "there are no Category attributes on this test. ");
        }

        [Test]
        [Category("TheCategory")]
        public void ASingleCategoryIsRetrieved()
        {
            var provider = new ResultingCategoryProvider();

            var result = provider.GetCategories(TestContext.CurrentContext.Test);

            result.Should().HaveCount(1, because: "there is a single Category on this test");
            result.First().Should().Be("TheCategory", because: "there is exactly one Category on this method called TheCategory");
        }

        [Test]
        [Category("TheCategory")]
        [Category("TheOtherCategory")]
        public void MultipleCategoriesAreRetrieved()
        {
            var provider = new ResultingCategoryProvider();

            var result = provider.GetCategories(TestContext.CurrentContext.Test);

            result.Should().HaveCount(2, because: "there are two categories on this Test");
            result.Cast<string>().Should().Contain("TheCategory", because: "it is one of the Categories on this test. ");
            result.Cast<string>().Should().Contain("TheOtherCategory", because: "it is one of the Categories on this test. ");
        }

        [Test]
        [Category("TheCategory")]
        [Category("TheCategory")]
        public void IdenticalCategoriesAreSquashedToASingleOne()
        {
            var provider = new ResultingCategoryProvider();

            var result = provider.GetCategories(TestContext.CurrentContext.Test);

            result.Should().HaveCount(1, because: "while there are two Category attributes, they are identical. ");
            result.Cast<string>().Should().Contain("TheCategory", because: "it is the only Category on this test. ");
        }
    }
}
