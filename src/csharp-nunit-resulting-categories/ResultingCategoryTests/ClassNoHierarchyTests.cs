using FluentAssertions;
using NUnit.Framework;
using System.Linq;

namespace ResultingCategoryTests
{
    public class WhenRetrievingCategoriesFromClassNotInAHierarchy
    {
        public class WhenThereAreNoCategories
        {
            [Test]
            public void NoCategoriesAreRetrieved()
            {
                var provider = new ResultingCategoryProvider();

                var result = provider.GetCategories(TestContext.CurrentContext.Test);

                result.Should().HaveCount(0, because: "there are no Category attributes on this Test Class. ");
            }
        }

        [Category("TheClassCategory")]
        public class WhenThereIsOneCategory
        {
            [Test]
            public void OneCategoryIsRetrieved()
            {
                var provider = new ResultingCategoryProvider();

                var result = provider.GetCategories(TestContext.CurrentContext.Test);

                result.Should().HaveCount(1, because: "there is a single Category on this Test Class");
                result.First().Should().Be("TheClassCategory", because: "there is exactly one Category on this Class called TheCategory");
            }
        }

        [Category("TheClassCategory")]
        [Category("TheOtherClassCategory")]
        public class WhenthereAreTwoCategories
        {
            [Test]
            public void TwoCategoriesAreRetrieved()
            {
                var provider = new ResultingCategoryProvider();

                var result = provider.GetCategories(TestContext.CurrentContext.Test);

                result.Should().HaveCount(2, because: "there are two categories on this Test Class");
                result.Cast<string>().Should().Contain("TheClassCategory", because: "it is one of the Categories on this Class. ");
                result.Cast<string>().Should().Contain("TheOtherClassCategory", because: "it is one of the Categories on this Class. ");
            }
        }

        [Category("TheClassCategory")]
        [Category("TheClassCategory")]
        public class WhenthereAreTwoIdenticalCategories
        {
            [Test]
            public void IdenticalCategoriesAreSquashedToASingleOne()
            {
                var provider = new ResultingCategoryProvider();

                var result = provider.GetCategories(TestContext.CurrentContext.Test);

                result.Should().HaveCount(1, because: "while there are two Category attributes, they are identical. ");
                result.Cast<string>().Should().Contain("TheClassCategory", because: "it is the only Category on this test. ");
            }
        }
    }
}
