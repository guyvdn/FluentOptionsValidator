using System.Diagnostics.CodeAnalysis;

namespace FluentOptionsValidator.UnitTests.ValidatorImplementations
{
    [ExcludeFromCodeCoverage]
    public class GenericOptionsValidatorImplementation<T> : FluentOptionsValidator<T> where T : class
    {
    }
}