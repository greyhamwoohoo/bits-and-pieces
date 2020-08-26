using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace IdiomaticTests.DataSource
{
    /// <summary>
    /// DataSource to discover Classes in an assembly (except those excluded)
    /// </summary>
    public class ConstructorFilterDataSourceAttribute : Attribute, ITestDataSource
    {
        private List<Assembly> Assemblies;

        public ConstructorFilterDataSourceAttribute(Type fromAssemblyContaining)
        {
            if (null == fromAssemblyContaining) throw new System.ArgumentNullException(nameof(fromAssemblyContaining));

            Assemblies = new List<Assembly>() { fromAssemblyContaining.Assembly };
        }

        public ConstructorFilterDataSourceAttribute(params Type[] fromAssembliesContaining)
        {
            if (null == fromAssembliesContaining) throw new System.ArgumentNullException(nameof(fromAssembliesContaining));
            if (fromAssembliesContaining.Length == 0) throw new System.ArgumentOutOfRangeException(nameof(fromAssembliesContaining), $"There must be more than one type specified. ");

            Assemblies = new List<Assembly>(fromAssembliesContaining.Select(t => t.Assembly).Distinct());
        }

        public IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            var excludedClasses = methodInfo.GetCustomAttributes<ConstructorFilterExcludeAttribute>().Select(e => e.Type);

            foreach (var assembly in Assemblies)
            {
                foreach (var cls in assembly.GetTypes().Where(t => t.IsClass).Except(excludedClasses))
                {
                    var constructors = cls.GetConstructors();

                    foreach (var constructor in constructors)
                    {
                        var result = new object[2];
                        result[0] = cls;
                        result[1] = constructor;

                        yield return result;
                    }
                }
            }

            yield break;
        }

        public string GetDisplayName(MethodInfo methodInfo, object[] data)
        {
            var className = ((Type)data[0]).FullName;
            var constructorParameters = string.Join(',', ((ConstructorInfo)data[1]).GetParameters().Select(f => f.ParameterType.Name));
            var result = $"{className} Constructor {constructorParameters}";
            return result;
        }
    }
}
