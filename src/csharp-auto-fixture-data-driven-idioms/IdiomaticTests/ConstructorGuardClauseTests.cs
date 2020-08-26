using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Idioms;
using IdiomaticTests.DataSource;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SomeProjectContainingClasses;
using SomeProjectContainingTwoClasses;
using System;
using System.Reflection;

// TODO: Unit test without datadriving

namespace IdiomaticTests
{
    [TestClass]
    public class ConstructorGuardClauseTests
    {
        [TestMethod]
        [ConstructorFilterDataSource(fromAssemblyContaining: typeof(ThisIsAClassWithOnePublicConstructor))]
        public void WillFindManyClassesInAnAssembly(Type _, ConstructorInfo constructor)
        {
            AssertConstructorGuardClauses(constructor);
        }

        [TestMethod]
        [ConstructorFilterDataSource(fromAssembliesContaining: typeof(ThisIsAClassWithOnePublicConstructor))]
        public void WillFindManyClassesInSingleAssembly(Type _, ConstructorInfo constructor)
        {
            AssertConstructorGuardClauses(constructor);
        }

        [TestMethod]
        [ConstructorFilterDataSource(typeof(ThisIsAClassWithOnePublicConstructor), typeof(ThisIsAClassWithFourPublicConstructors))]
        public void WillFindManyClassesInManyAssemblies(Type _, ConstructorInfo constructor)
        {
            AssertConstructorGuardClauses(constructor);
        }

        [TestMethod]
        [ConstructorFilterDataSource(fromAssemblyContaining: typeof(ThisIsAClassWithOnePublicConstructor))]
        [ConstructorFilterExclude(typeof(ThisIsAClassWithOnePublicConstructor))]
        public void WillExcludeClasses(Type _, ConstructorInfo constructor)
        {
            AssertConstructorGuardClauses(constructor);
        }

        private void AssertConstructorGuardClauses(ConstructorInfo constructor)
        {
            var fixture = new Fixture();

            fixture.Customize(new AutoNSubstituteCustomization());

            var assertThatAllMembersHaveGuards = new GuardClauseAssertion(fixture);

            // Act, Assert
            assertThatAllMembersHaveGuards.Verify(constructor);
        }
    }
}
