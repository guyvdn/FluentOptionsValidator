using FluentAssertions;
using FluentOptionsValidator.UnitTests.ValidatorImplementations;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace FluentOptionsValidator.UnitTests.ServiceCollectionExtensionsTests
{
    [TestFixture]
    public class It_should_register_the_correct_validators
    {
        private ServiceCollection _serviceCollection;

        [SetUp]
        public void Setup()
        {
            _serviceCollection = new ServiceCollection();
            _serviceCollection.RegisterFluentOptionsValidators<ClassToValidate>();
        }

        [Test]
        public void It_should_register_the_FluentOptionsValidator_implementation()
        {
            _serviceCollection.Should().OnlyContain(x =>
                x.ServiceType == typeof(IValidateOptions<ClassToValidate>) &&
                x.ImplementationType == typeof(FluentOptionsValidatorImplementation) &&
                x.Lifetime == ServiceLifetime.Transient
            );
        }

        [Test]
        public void It_should_be_able_to_resolve_the_validator_as_IValidateOptions()
        {
            var provider = _serviceCollection.BuildServiceProvider();
            var validator = provider.GetRequiredService<IValidateOptions<ClassToValidate>>();
            validator.Should().NotBeNull();
        }

        [Test]
        public void It_should_not_register_abstract_implementations_of_FluentOptionsValidator()
        {
            _serviceCollection.Should().NotContain(x =>
                x.ImplementationType == typeof(AbstractFluentOptionsValidatorImplementation)
            );
        }

        [Test]
        public void It_should_not_register_generic_implementations_of_FluentOptionsValidator()
        {
            _serviceCollection.Should().NotContain(x =>
                x.ImplementationType == typeof(GenericOptionsValidatorImplementation<>)
            );
        }

        [Test]
        public void It_should_not_register_classes_that_only_implement_IValidateOptions()
        {
            _serviceCollection.Should().NotContain(x =>
                x.ImplementationType == typeof(OptionsValidatorImplementation)
            );
        }

        [Test]
        public void It_should_not_register_classes_that_only_implement_AbstractValidator()
        {
            _serviceCollection.Should().NotContain(x =>
                x.ImplementationType == typeof(AbstractValidator<ClassToValidate>)
            );
        }
    }
}