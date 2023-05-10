using BrewUp.Wharehouses.Rest.Models;
using FluentValidation;

namespace BrewUp.Wharehouses.Rest.Validators
{
    public class SayHelloValidator : AbstractValidator<HelloRequest>
    {
        public SayHelloValidator()
        {
            RuleFor(h => h.Name).NotEmpty().MaximumLength(50);
        }
    }
}