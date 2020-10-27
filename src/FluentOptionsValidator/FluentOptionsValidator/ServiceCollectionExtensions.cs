using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FluentOptionsValidator
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds all <see cref="FluentOptionsValidator{TOptions}"/> in the assembly of the type specified by the generic parameter
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="lifetime">The <see cref="ServiceLifetime"/> of the validators. The default is <see cref="ServiceLifetime.Transient"/></param>
        /// <returns></returns>
        public static IServiceCollection RegisterFluentOptionsValidators<T>(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            return services.RegisterFluentOptionsValidators(typeof(T).GetTypeInfo().Assembly, lifetime);
        }

        public static IServiceCollection RegisterFluentOptionsValidators(this IServiceCollection services, Assembly assembly, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
        {
            var fluentOptionsValidatorType = typeof(IFluentOptionsValidator<>);
            var validateOptionsType = typeof(IValidateOptions<>);

            foreach (var implementationType in assembly.GetExportedTypes())
            {
                if (implementationType.IsAbstract || implementationType.IsGenericTypeDefinition)
                    continue;

                var interfaces = implementationType.GetInterfaces();
                if (!interfaces.Any(type => type.ImplementsGenericType(fluentOptionsValidatorType)))
                    continue;

                var serviceType = interfaces.FirstOrDefault(type => type.ImplementsGenericType(validateOptionsType));

                services.Add(new ServiceDescriptor(serviceType, implementationType, serviceLifetime));
            }

            return services;
        }

        private static bool ImplementsGenericType(this Type type, Type fluentOptionsValidatorType)
        {
            return type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == fluentOptionsValidatorType;
        }
    }
}