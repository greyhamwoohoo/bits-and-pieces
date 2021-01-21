# Resulting Categories for NUnit
The context: when recently working on a monolith, some of our tests were disruptive to run in certain environments. 

Our first line of defense was obviously to exclude categories with --TestCaseFilter and --where.

The second line of defense was to dynamically check whether tests were tagged with certain Categories during test execution (in SetUp); if so, we would Assert.Ignore() the test so it never ran. 

This .Net test project shows some code for the second line of defense. 

## Usage
```
var categories = new ResultingCategoryProvider().GetCategories(TestContext.CurrentContext.Test);

if(categories.ToList().Contains("TheForbiddenCategory")) {
    Assert.Ignore($"The test is not allowed to run. ");
}
```

## Limitations
See the Unit Test for coverage. 

### Containment
NUnit Test Categories do not include or exclude based on nested types/classes (so neither does this code).

For example - given a class like this: 

```
        [Category("OuterClassCategory")]
        public class OuterTestClass
        {
            public class InnerTestClass
            {
                [Test]
                public void CanRetrieveOuterClassCategory()
                {

                }
            }
        }
```

The following command will NOT run the test:

```
vstest.console.exe CategoryInterrogator.dll --TestCaseFilter:TestCategory=OuterClassCategory
```
