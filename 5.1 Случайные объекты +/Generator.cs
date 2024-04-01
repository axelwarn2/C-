using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Reflection.Randomness
{
    public class FromDistributionAttribute : Attribute
    {
        public Type DistributionType { get; }
        public object[] Parameters { get; }

        public FromDistributionAttribute(Type distributionType, params object[] parameters)
        {
            DistributionType = distributionType;
            Parameters = parameters;
        }
    }

    public class Generator<T> where T : new()
    {
        private readonly Dictionary<string, Func<Random, object>> propertyGenerators = new Dictionary<string, Func<Random, object>>();

        public Generator()
        {
            var properties = typeof(T).GetProperties().Where(prop => prop.GetCustomAttribute<FromDistributionAttribute>() != null);
            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute<FromDistributionAttribute>();
                var distributionType = attribute.DistributionType;
                var parameters = attribute.Parameters;

                if (!typeof(IContinuousDistribution).IsAssignableFrom(distributionType))
                {
                    throw new ArgumentException($"Invalid distribution type '{distributionType.Name}' for property '{property.Name}'");
                }

                var ctorParams = parameters.Select(p => p.GetType()).ToArray();
                var distributionConstructor = distributionType.GetConstructor(ctorParams);
                if (distributionConstructor == null)
                {
                    throw new ArgumentException($"Invalid distribution parameters for property '{distributionType}'");
                }

                var expectedParamCount = distributionConstructor.GetParameters().Length;
                if (parameters.Length != expectedParamCount)
                {
                    throw new ArgumentException($"Invalid distribution parameters for property '{property.Name}' of type '{distributionType.Name}'");
                }

                propertyGenerators[property.Name] = rnd =>
                {
                    var distribution = (IContinuousDistribution)Activator.CreateInstance(distributionType, parameters);
                    return distribution.Generate(rnd);
                };
            }
        }

        public T Generate(Random rnd)
        {
            var instance = new T();
            foreach (var propertyGenerator in propertyGenerators)
            {
                var property = typeof(T).GetProperty(propertyGenerator.Key);
                var value = propertyGenerator.Value.Invoke(rnd);
                property.SetValue(instance, Convert.ChangeType(value, property.PropertyType));
            }
            return instance;
        }
    }
}
