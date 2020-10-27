# FluentOptionsValidator
Combining FluentValidation and IValidateOptions into one

## Usage

Inherit from `FluentOptionsValidator<>` where you would normaly use `AbstractValidator<>`

```csharp
public class FluentOptionsValidatorImplementation : FluentOptionsValidator<ClassToValidate>
{
    public FluentOptionsValidatorImplementation()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}
```

Register all FluentOptionsValidators for an assembly with the included IServiceCollection extension method `RegisterFluentOptionsValidators`

```csharp
_serviceCollection.RegisterFluentOptionsValidators<ClassToValidate>();
```