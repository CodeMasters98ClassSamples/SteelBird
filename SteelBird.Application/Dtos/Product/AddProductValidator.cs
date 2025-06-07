using FluentValidation;

namespace SteelBird.Application.Dtos.Product;

public class AddProductValidator : AbstractValidator<AddProduct>
{
    public AddProductValidator()
    {
        RuleFor(x => x.Name)
               .NotEmpty()
               .NotNull()
               .WithMessage("Please enter valid name");
    }
}
