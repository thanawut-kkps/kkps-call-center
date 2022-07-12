using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac.Core;
using System.Reflection;

namespace Phatra.Core.Logging
{
    public class LoggingModule : Autofac.Module
    {

        //protected override void Load(ContainerBuilder builder)
        //{
        //    builder.Register((c, p) => GetLogger(p.TypedAs<Type>()));
        //}

        private static void InjectLoggerProperties(object instance)
        {
            var instanceType = instance.GetType();

            // Get all the injectable properties to set.
            // If you wanted to ensure the properties were only UNSET properties,
            // here's where you'd do it.
            var properties = instanceType
              .GetProperties(BindingFlags.Public | BindingFlags.Instance)
              .Where(p => p.PropertyType == typeof(ILog) && p.CanWrite && p.GetIndexParameters().Length == 0);

            // Set the properties located.
            foreach (var propToSet in properties)
            {
                propToSet.SetValue(instance, GetLogger(instanceType), null);
            }
        }

        private static void OnComponentPreparing(object sender, PreparingEventArgs e)
        {
            var t = e.Component.Activator.LimitType;
            e.Parameters = e.Parameters.Union(
              new[]
                      {
                        new ResolvedParameter((p, i) => p.ParameterType == typeof(ILog), (p, i) => GetLogger(t)),
                      });
        }

        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
        {
            // Handle constructor parameters.
            registration.Preparing += OnComponentPreparing;

            // Handle properties.
            registration.Activated += (sender, e) => InjectLoggerProperties(e.Instance);
        }

        public static ILog GetLogger(Type type)
        {
            return new Log4NetLogger(type);
        }
    }

}
