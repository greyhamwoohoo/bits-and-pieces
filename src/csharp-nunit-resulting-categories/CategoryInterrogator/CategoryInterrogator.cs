using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using static NUnit.Framework.TestContext;

namespace CategoryInterrogator
{
    /// <summary>
    /// Union distinct Category attributes on the test, class and class hierarchy. 
    /// </summary>
    public class CategoryInterrogator
    {
        public CategoryInterrogator()
        {
        }

        public IEnumerable<string> Interrogate(TestAdapter test)
        {
            var classType = GetClasses()
                .Where(t => t.FullName == test.ClassName)
                .FirstOrDefault();

            return Interrogate(test, classType);
        }

        public IEnumerable<string> Interrogate(TestAdapter test, Type inClass)
        {
            var result = new List<string>();

            // Class Hierarchy
            var classes = ToClassHierarchy(inClass);
            foreach (var c in classes)
            {
                result.AddRange(c
                    .GetCustomAttributes(typeof(CategoryAttribute), inherit: false)
                    .Select(a => ((CategoryAttribute)a).Name));
            };

            // Method
            foreach (var key in test.Properties.Keys.Where(k => k == "Category"))
            {
                var value = test.Properties[key];
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
