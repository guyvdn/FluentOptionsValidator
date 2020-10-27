using System.Diagnostics.CodeAnalysis;
using FluentValidation;

namespace FluentOptionsValidator.UnitTests.ValidatorImplementations
{
    [ExcludeFromCodeCoverage]
    public class FluentValidatorImplementation : AbstractValidator<ClassToValidate>
    {
        public FluentValidatorImplementation()
        {
            RuleFor(x => x.Name).NotNull();
        }
    }
}