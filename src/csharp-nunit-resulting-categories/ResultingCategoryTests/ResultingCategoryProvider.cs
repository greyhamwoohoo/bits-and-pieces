using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using static NUnit.Framework.TestContext;

namespace ResultingCategoryTests
{
    public class ResultingCategoryProvider
    {
        /// <summary>
        /// Get the Categories from the Method, Class and Base Classes. 
        /// </summary>
        /// <param name="testAdapter">NUnit Test Adapter (from the Nunit test context)</param>
        /// <returns>A distinct union of the categories on the test method, its class and any base classes. </returns>
        public IEnumerable<string> GetCategories(TestAdapter testAdapter)
        {
            var classType = GetClasses().Where(t => t.FullName == testAdapter.ClassName).FirstOrDefault();
            return GetCategories(testAdapter, classType);
        }

        /// <summary>
        /// Get the Categories from the Method, Class and Base Classes. 
        /// </summary>
        /// <param name="testAdapter">NUnit Test Adapter (from the Nunit test context)</param>
        /// <param name="classType">Optional test class type. If null, the entire AppDomain and Assemblies will be scanned. </param>
        /// <returns>A distinct union of the categories on the test method, its class and any base classes. </returns>
        public IEnumerable<string> GetCategories(TestAdapter testAdapter, Type classType)
        {
            var result = new List<string>();

            // Class Hierarchy
            var classes = ToClassHierarchy(classType);
            foreach(var c in classes)
            {
                result.AddRange(c
                    .GetCustomAttributes(typeof(CategoryAttribute), inherit: false)
                    .Select(a => ((CategoryAttribute)a).Name));
            };

            // Method
            foreach (var key in testAdapter.Properties.Keys)
            {
                var value = testAdapter.Properties[key];
                result.AddRange(value.Select(o => o.ToString()));
            }

            return result.Distinct();
        }

        private IEnumerable<Type> GetClasses()
        {
            var classes = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes().Where(f => f.IsClass));

            return classes;
        }

        private IEnumerable<Type> ToClassHierarchy(Type classType)
        {
            var result = new List<Type>();
            if (classType == null) return result;

            var currentType = classType;
            do
            {
                result.Add(currentType);

                currentType = currentType.BaseType;

            } while (currentType != null);

            result.Reverse();

            return result;
        }
    }
}
