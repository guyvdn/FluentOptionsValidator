using System.Diagnostics.CodeAnalysis;
using FluentValidation;

namespace FluentOptionsValidator.UnitTests.ValidatorImplementations
{
    [ExcludeFromCodeCoverage]
    public class FluentOptionsValidatorImplementation : FluentOptionsValidator<ClassToValidate>
    {
        public FluentOptionsValidatorImplementation()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}