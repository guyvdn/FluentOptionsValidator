using FluentAssertions;
using FluentOptionsValidator.UnitTests.ValidatorImplementations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace FluentOptionsValidator.UnitTests.ServiceCollectionExtensionsTests
{
    [TestFixture]
    public class It_should_register_the_validators_with_the_correct_lifetime
    {
        [TestCase(ServiceLifetime.Transient)]
        [TestCase(ServiceLifetime.Scoped)]
        [TestCase(ServiceLifetime.Singleton)]
        public void For_LifeTime(ServiceLifetime serviceLifetime)
        {
            var _serviceCollection = new ServiceCollection();
            _serviceCollection.RegisterFluentOptionsValidators<ClassToValidate>(serviceLifetime);

            _serviceCollection.Should().OnlyContain(x =>
                x.ServiceType == typeof(IValidateOptions<ClassToValidate>) &&
                x.ImplementationType == typeof(FluentOptionsValidatorImplementation) &&
                x.Lifetime == serviceLifetime
            );
        }
    }
}