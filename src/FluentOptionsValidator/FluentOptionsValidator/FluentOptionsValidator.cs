using System.Linq;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace FluentOptionsValidator
{
    /// <summary>
    /// Interface used to validate options.
    /// </summary>
    /// <typeparam name="TOptions">The options type to validate.</typeparam>
    public interface IFluentOptionsValidator<TOptions> : IValidateOptions<TOptions> where TOptions : class { }

    /// <summary>
    /// Base class for fluent options validators.
    /// </summary>
    /// <typeparam name="TOptions">The type of the object being validated</typeparam>
    public abstract class FluentOptionsValidator<TOptions> : AbstractValidator<TOptions>, IFluentOptionsValidator<TOptions> where TOptions : class
    {
        public ValidateOptionsResult Validate(string name, TOptions value)
        {
            var validationResult = Validate(value);

            if (validationResult.IsValid)
                return ValidateOptionsResult.Success;

            return ValidateOptionsResult.Fail(validationResult.Errors.Select(x => x.ErrorMessage));
        }
    }
}
