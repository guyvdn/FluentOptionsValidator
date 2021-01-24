using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Options;

namespace FluentOptionsValidator.UnitTests.ValidatorImplementations
{
    [ExcludeFromCodeCoverage]
    public class OptionsValidatorImplementation: IValidateOptions<ClassToValidate>
    {
        public ValidateOptionsResult Validate(string name, ClassToValidate options)
        {
            if (string.IsNullOrEmpty(options.Name))
                return  ValidateOptionsResult.Fail("Name should not be empty");

            return ValidateOptionsResult.Success;
        }
    }
}