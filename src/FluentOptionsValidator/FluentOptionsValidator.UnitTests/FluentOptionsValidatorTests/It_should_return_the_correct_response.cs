using System.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentOptionsValidator.UnitTests.ValidatorImplementations;
using NUnit.Framework;

namespace FluentOptionsValidator.UnitTests.FluentOptionsValidatorTests
{
    [TestFixture]
    public class It_should_return_the_correct_response
    {
        [Test]
        public void When_the_options_are_valid()
        {
            var options = new ClassToValidate { Name = "Not empty" };

            var implementation = new FluentOptionsValidatorImplementation();
            var validationResult = implementation.Validate("a name", options);

            using (new AssertionScope())
            {
                validationResult.Failed.Should().BeFalse();
                validationResult.Failures.Should().BeNull();
            }
        }  
        
        [Test]
        public void When_the_options_are_invalid()
        {
            var options = new ClassToValidate { Name = string.Empty };

            var implementation = new FluentOptionsValidatorImplementation();
            var validationResult = implementation.Validate("a name", options);

            using (new AssertionScope())
            {
                validationResult.Failed.Should().BeTrue();
                validationResult.Failures.Count().Should().Be(1);
                validationResult.Failures.First().Should().Be("'Name' must not be empty.");
            }
        }
    }
}