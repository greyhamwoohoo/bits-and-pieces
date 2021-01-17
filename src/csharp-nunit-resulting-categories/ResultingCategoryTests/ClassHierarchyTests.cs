using FluentAssertions;
using NUnit.Framework;
using System.Linq;

namespace ResultingCategoryTests
{
    public class WhenRetrievingCategoriesFromClassesInAHierarchy
    {
        public class BaseClassWithNoCategories
        {
        }

        [Category("TheBaseClassCategory")]
        public class BaseClassWithOneCategory
        {
        }

        public class WhenThereAreNoCategories : BaseClassWithNoCategories
        {
            [Test]
            public void NoCategoriesAreRetrieved()
            {
                var provider = new ResultingCategoryProvider();

                var result = provider.GetCategories(TestContext.CurrentContext.Test);

                result.Should().HaveCount(0, because: "there are no Category attributes on this Test Class hierarchy. ");
            }
        }

        public class WhenThereIsCategoryOnTheBaseClass : BaseClassWithOneCategory
        {
            [Test]
            public void OneCategoryIsRetrieved()
            {
                var provider = new ResultingCategoryProvider();

                var result = provider.GetCategories(TestContext.CurrentContext.Test);

                result.Should().HaveCount(1, because: "there is a single Category on this Test Class hierarchy");
                result.First().Should().Be("TheBaseClassCategory", because: "there is exactly one Category on this Test Method hierarchy");
            }
        }

        [Category("TheClassCategory")]
        public class WhenThereIsACategoryOnEachClass : BaseClassWithOneCategory
        {
            [Test]
            public void TwoCategoriesAreRetrieved()
            {
                var provider = new ResultingCategoryProvider();

                var result = provider.GetCategories(TestContext.CurrentContext.Test);

                result.Should().HaveCount(2, because: "there are two categories on this Test Class Hierarchy. ");
                result.Cast<string>().Should().Contain("TheClassCategory", because: "it is one of the Categories on this Class Hierarchy. ");
                result.Cast<string>().Should().Contain("TheBaseClassCategory", because: "it is one of the Categories on this Class Hierarchy. ");
            }
        }
    }
}
