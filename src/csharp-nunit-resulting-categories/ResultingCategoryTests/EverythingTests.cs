using FluentAssertions;
using NUnit.Framework;
using System.Linq;

namespace ResultingCategoryTests
{
    public class WhenLotsOfThingsHaveCategories
    {
        [Category("TheUltimateBaseClassCategory")]
        public class UltimateBaseClassWithCategories { }

        public class PenultimateBaseClassWithNoCategories : UltimateBaseClassWithCategories { }

        [Category("TheIntermediateBaseClassCategory")]
        public class IntermediateBaseClassWithoneCategory : PenultimateBaseClassWithNoCategories { }

        [Category("TheClassCategory")]
        public class WhenTheHierarchyHasMixedCategories : IntermediateBaseClassWithoneCategory
        {
            [Test]
            [Category("TheMethodCategory")]
            public void TheCategoriesAreUnioned()
            {
                var result = new ResultingCategoryBuilder()
                    .Build(fromTest: TestContext.CurrentContext.Test);

                result.Should().HaveCount(4, because: "there are two categories on this Test");
                result.Cast<string>().Should().Contain("TheMethodCategory", because: "it is one of the Categories on this hierarchy. ");
                result.Cast<string>().Should().Contain("TheClassCategory", because: "it is one of the Categories on this hierarchy. ");
                result.Cast<string>().Should().Contain("TheIntermediateBaseClassCategory", because: "it is one of the Categories on this hierarchy. ");
                result.Cast<string>().Should().Contain("TheUltimateBaseClassCategory", because: "it is one of the Categories on this hierarchy. ");
            }
        }
    }
}
